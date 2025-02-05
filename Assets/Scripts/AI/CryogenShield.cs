using UnityEngine;

public class CryogenShield : EnemyScript
{
    public bool m_IsShieldUp = true;

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
