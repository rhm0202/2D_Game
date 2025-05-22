using UnityEngine;
using System.Collections;

public class PlayerAction : MonoBehaviour
{
    // 플레이어 움직임
    public float speed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    //플레이어 공격
    public GameObject bulletPrefab;           // 투사체 프리팹
    public Transform firePoint;               // 발사 위치
    public float bulletSpeed = 10f;
    public Transform CrfirePoint;
    public float fireDelay = 0.5f;
    private float nextFireTime = 0f;

    //플레이어 피격시
    private bool isHit = false;
    public float hitCooldown = 1f;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.2f;
    private bool isKnockbacked = false;
    [SerializeField] private float invincibleTime = 1f;
    [SerializeField] private float blinkInterval = 0.1f;

    private bool isInvincible = false;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        Move(moveInput);
        Jumpping();

        //앉기 관련 로직
        bool crouching = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        animator.SetBool("IsCrouching", crouching);

        bool canShootTime = Time.time >= nextFireTime;
        bool haveAmmo = GameManager.Instance.playerResource.currentAmmo > 0;

        //총알 관련 로직
        if (Input.GetKeyDown(KeyCode.LeftControl) && moveInput == 0 && isGrounded)
        {
            if (haveAmmo) {
                if (canShootTime)
                {
                    //플레이어 공격 구현
                    if (crouching)
                    {
                        animator.SetTrigger("IsCrShooting");
                        CrShoot();

                    }
                    else
                    {
                        animator.SetTrigger("IsShooting");
                        Shoot();
                    }

                    GameManager.Instance.UseAmmo(1);
                    UIManager.Instance.UpdateAmmo();
                    nextFireTime = Time.time + fireDelay;

                }
            }
            else if (!haveAmmo)
            {
                GameManager.Instance.Reload();
                UIManager.Instance.UpdateAmmo();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) // R키로 리로드
        {
            GameManager.Instance.Reload();
            UIManager.Instance.UpdateAmmo();
        }

        
    }

    //피격 관련 로직
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && !isHit)
        {

            isHit = true;
            GameManager.Instance.TakeDamage(1);
            UIManager.Instance.UpdateHP();
            animator.SetTrigger("IsHurt");

            KnockbackFrom(other.transform.position);     
            StartCoroutine(BlinkRoutine());

            Invoke(nameof(ResetHit), hitCooldown);
        }
    }

    void Shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 방향 처리 (현재 바라보는 방향 기준)
        float direction = transform.localScale.x > 0 ? 1f : -1f;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(direction * bulletSpeed, 0f);

        Vector3 bulletScale = bullet.transform.localScale;
        bulletScale.x = direction;
        bullet.transform.localScale = bulletScale;
    }

    void CrShoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, CrfirePoint.position, Quaternion.identity);

        // 방향 처리 (현재 바라보는 방향 기준)
        float direction = transform.localScale.x > 0 ? 1f : -1f;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(direction * bulletSpeed, 0f);

        Vector3 bulletScale = bullet.transform.localScale;
        bulletScale.x = direction;
        bullet.transform.localScale = bulletScale;
    }

    void Move(float moveInput)
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        animator.SetBool("IsMoving", true);

        if (moveInput == 0)
        {
            animator.SetBool("IsMoving", false);
        }

        if (moveInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = moveInput > 0 ? 1 : -1;
            transform.localScale = scale;
        }

    }

    void Jumpping()
    {
        //점프 구현
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetKeyDown(KeyCode.LeftAlt) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        animator.SetBool("IsJumping", !isGrounded);
    }

    void ResetHit()
    {
        isHit = false;
    }
    public void KnockbackFrom(Vector2 sourcePosition)
    {
        if (isKnockbacked) return;

        float dir = Mathf.Sign(transform.position.x - sourcePosition.x); // -1 or 1
        Vector2 knockDir = new Vector2(dir, 0f);

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);

        isKnockbacked = true;
        Invoke(nameof(EndKnockback), knockbackDuration);
    }

    void EndKnockback()
    {
        isKnockbacked = false;
        rb.linearVelocity = Vector2.zero;
    }

    IEnumerator BlinkRoutine()
    {
        isInvincible = true;
        float elapsed = 0f;

        while (elapsed < invincibleTime)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        sr.enabled = true;
        isInvincible = false;
    }
}
