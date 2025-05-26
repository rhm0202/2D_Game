using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "PlayerResourceData", menuName = "Game/PlayerResource")]


public class PlayerResource : ScriptableObject
{
    public int maxHP = 5;
    public int currentHP = 5;

    public int maxAmmo = 5;
    public int currentAmmo = 5;

    public int itemCount = 0;

    public void ResetData()
    {
        currentHP = maxHP;
        currentAmmo = maxAmmo;
        itemCount = 0;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        if (currentHP == 0)
        {
            GameOver(); // 체력 0이면 게임오버로 전환
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;
    }

    public void UseAmmo(int amount)
    {
        currentAmmo -= amount;
        if (currentAmmo < 0) currentAmmo = 0;
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }

    public void AddItem(int count)
    {
        itemCount += count;
    }
    void GameOver()
    {
        currentHP = maxHP;
        currentAmmo = maxAmmo;
        itemCount = 0;
        SceneManager.LoadScene("GameOver");
    }
}