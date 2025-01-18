using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private HealthbarScript m_playerHealthbar;
    [SerializeField] private int m_maxPlayerHealth;
    [SerializeField] private int m_currentHealth;

    private DamageFlash m_flash;

    private void Awake()
    {
        m_currentHealth = m_maxPlayerHealth;
        m_flash = GetComponent<DamageFlash>();
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;
        UpdatePlayerHealthbar();
        if (m_currentHealth <= 0)
            Death();
        else
            m_flash.Flash();
    }

    private void UpdatePlayerHealthbar()
    {
        float percentage = (float)m_currentHealth / (float)m_maxPlayerHealth;
        m_playerHealthbar.SetHealthByPercentage(percentage);
    }

    private void Death()
    {
        m_currentHealth = 0;
        GameManager.instance.LostGame();
    }
}
