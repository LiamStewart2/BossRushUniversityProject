using UnityEngine;

[RequireComponent(typeof(DamageFlash))]

public class EnemyScript : MonoBehaviour
{
    [Header("Enemy Attributes")]
    [SerializeField] protected int m_maxHealth;

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

        updateHealthbar();

        if(m_currentHealth < 0)
        {
            m_currentHealth = 0;
            GameManager.instance.healthbarScript.Disable();
            Destroy(gameObject);
        }
    }
    public virtual void updateHealthbar()
    {
        float percentage = (float)m_currentHealth / (float)m_maxHealth;
        GameManager.instance.healthbarScript.SetHealthByPercentage(percentage);
    }
}
