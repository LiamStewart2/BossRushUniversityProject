using UnityEngine;

/// <summary>
/// Parent script for all enemies
/// </summary>

[RequireComponent(typeof(DamageFlash))]
public class EnemyScript : MonoBehaviour
{
    [Header("Enemy Attributes")]
    [SerializeField] protected int m_maxHealth;
    [SerializeField] protected bool m_isBoss;

    protected int m_currentHealth;
    private DamageFlash m_flash;

    protected virtual void Awake()
    {
        m_currentHealth = m_maxHealth;
        m_flash = GetComponent<DamageFlash>();
    }

    public virtual void takeDamage(int damage)
    {
        m_currentHealth -= damage;
        m_flash.Flash();

        if(m_isBoss)
            updateHealthbar();

        if (m_currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        m_currentHealth = 0;
        if (m_isBoss)
            GameManager.instance.m_healthbarScript.Disable();
        Destroy(gameObject);
    }

    public virtual void updateHealthbar()
    {
        float percentage = (float)m_currentHealth / (float)m_maxHealth;
        GameManager.instance.m_healthbarScript.SetHealthByPercentage(percentage);
    }

    public void OnEnable()
    {
        m_flash.Flash();
    }
}
