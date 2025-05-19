using UnityEngine;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //좌우 움직임 구현
        float moveInput = Input.GetAxisRaw("Horizontal");

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

        //점프 구현
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetKeyDown(KeyCode.LeftAlt) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        animator.SetBool("IsJumping", !isGrounded);

        //앉기 구현
        bool crouching = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        animator.SetBool("IsCrouching", crouching);

        //플레이어 공격 구현
        if (Input.GetKeyDown(KeyCode.LeftControl) && moveInput == 0 && isGrounded)
        {
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
}
