using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    public int maxHP = 5;
    private int currentHP;

    public bool displayHpBar = false; 
    public GameObject hpBarUI;      
    private Slider hpSlider;
    private GameObject hpInstance;

    void Start()
    {
        currentHP = maxHP;

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
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject); 
    }


}
