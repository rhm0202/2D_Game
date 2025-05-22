using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int maxHP = 5;

    private int currentHP;

    public Transform target; // 플레이어 타겟

    //체력 UI
    public bool displayHpBar = false;
    public GameObject hpBarUI;
    private Slider hpSlider;
    private GameObject hpInstance;

    private bool diecheck = false;

    private Animator animator;

    void Start()
    {
        currentHP = maxHP;

        animator = GetComponent<Animator>();

        if (target == null && GameObject.FindWithTag("Player") != null)
            target = GameObject.FindWithTag("Player").transform;

        if (displayHpBar && hpBarUI != null)
        {
            hpInstance = Instantiate(hpBarUI, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            hpInstance.transform.SetParent(transform);
            hpInstance.transform.localPosition = Vector3.up * 1.2f;

            Canvas canvas = hpInstance.GetComponentInChildren<Canvas>();
            if (canvas != null)
                canvas.transform.localScale = Vector3.one * 0.15f;

            float objectWidth = 1f; // 기본값

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Collider2D col = GetComponent<Collider2D>();

            float spriteWidth = sr != null ? sr.bounds.size.x : 0f;
            float colliderWidth = col != null ? col.bounds.size.x : 0f;

            objectWidth = Mathf.Max(spriteWidth, colliderWidth, 8f);

            hpSlider = hpInstance.GetComponentInChildren<Slider>();
            if (hpSlider != null)
            {
                RectTransform rt = hpSlider.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(objectWidth * 1.5f, rt.sizeDelta.y); // 원하는 배수만큼 늘림
                hpSlider.maxValue = maxHP;
                hpSlider.value = currentHP;
            }

            hpInstance.SetActive(false); // 초기엔 꺼둠
        }
    }

    void Update()
    {
        if (target != null)
        {
            // 플레이어 방향으로 이동
            Vector2 dir = (target.position - transform.position).normalized;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            FlipToMoveDirection(dir);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        if (displayHpBar && hpSlider != null)
        {
            hpSlider.value = currentHP;
            if (currentHP < maxHP)
                hpInstance.SetActive(true);
            else
                hpInstance.SetActive(false);
        }

        if (currentHP <= 0)
        {
            if (hpInstance != null)
                Destroy(hpInstance);

            Die();
        }
            
    }

    void Die()
    {
        if(diecheck == false)
        {
            GameManager.Instance.kill++;
            diecheck = true;
        }

        if (animator != null)
        {
            animator.SetTrigger("Die"); 
        }
        
        StartCoroutine(DelayedDestroy(0.5f));
        
    }

    private System.Collections.IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void FlipToMoveDirection(Vector2 direction)
    {
        if (direction.x == 0) return;

        bool movingRight = direction.x > 0;
        bool facingRight = transform.localScale.x > 0;

        if (movingRight == facingRight)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }


}