using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private HealthbarScript m_playerHealthbar;
    [SerializeField] private int m_maxPlayerHealth;
    [SerializeField] private int m_currentHealth;

    private void Awake()
    {
        m_currentHealth = m_maxPlayerHealth;
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;
        UpdatePlayerHealthbar();
        if (m_currentHealth <= 0)
            Death();
    }

    private void UpdatePlayerHealthbar()
    {
        float percentage = (float)m_currentHealth / (float)m_maxPlayerHealth;
        m_playerHealthbar.SetHealthByPercentage(percentage);
    }

    private void Death()
    {
        m_currentHealth = 0;
    }
}
