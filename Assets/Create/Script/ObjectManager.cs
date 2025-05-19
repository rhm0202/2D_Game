using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    public int maxHP = 5;
    private int currentHP;

    public bool displayHpBar = false; 
    public GameObject hpBarUI;      
    private Slider hpSlider;

    void Start()
    {
        currentHP = maxHP;

        if (displayHpBar && hpBarUI != null)
        {
            GameObject hpInstance = Instantiate(hpBarUI, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            hpSlider = hpInstance.GetComponentInChildren<Slider>();

            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;

            hpInstance.transform.SetParent(GameObject.Find("Canvas").transform, false); // UI 캔버스가 있어야 함
        }
        else if (hpBarUI != null)
        {
            hpBarUI.SetActive(false);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        if (displayHpBar && hpSlider != null)
        {
            hpSlider.value = currentHP;
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
