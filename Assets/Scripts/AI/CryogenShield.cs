using UnityEngine;

/// <summary>
/// Cryogens shield
/// </summary>

public class CryogenShield : EnemyScript
{

    [SerializeField] private AudioSource[] m_HitSounds;

    public bool m_IsShieldUp = true;

    public override void takeDamage(int damage)
    {
        base.takeDamage(damage);

        int hitSoundIndex = Random.Range(0, m_HitSounds.Length);
        m_HitSounds[hitSoundIndex].Play();
    }

    public override void Die()
    {
        BreakShield();
    }

    public void RespawnShield()
    {
        if (m_IsShieldUp == false)
        {
            m_IsShieldUp = true;
            gameObject.SetActive(true);
            m_currentHealth = m_maxHealth;
        }
        
    }

    private void BreakShield()
    {
        if(m_IsShieldUp)
        {
            m_IsShieldUp = false;
            gameObject.SetActive(false);
        }
    }
}
