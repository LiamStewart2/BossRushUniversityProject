using UnityEngine;

/// <summary>
/// Handles the players currently held gun
/// </summary>

public class GunManager : MonoBehaviour
{
    private GameObject m_currentEquippedGun;
    private GunScript m_currentGunScript;
    private CurrentGunHolder m_currentGunHolder;

    private void Awake()
    {
        m_currentGunHolder = GameObject.FindGameObjectWithTag("CurrentGun").GetComponent<CurrentGunHolder>();
        if (m_currentGunHolder.m_gun != null)
            EquipGun(m_currentGunHolder.m_gun);
    }

    // Destroys the last held gun object then equips a new gun
    public void EquipGun(GameObject newGun)
    {
        if (newGun.GetComponent<GunScript>() != null)
        {
            Destroy(m_currentEquippedGun);
            m_currentEquippedGun = Instantiate(newGun, transform);
            m_currentGunScript = m_currentEquippedGun.GetComponent<GunScript>();
            m_currentGunHolder.m_gun = newGun;
        }
        else
        {
            Debug.LogError("[INVALID GUN] - " + newGun.name + " has no Gun Script");
        }
    }

    // Just destroyes the last held gun object
    public void UnequipGun()
    {
        Destroy(m_currentEquippedGun);
        m_currentEquippedGun = null;
        m_currentGunScript = null;
        m_currentGunHolder.m_gun = null;
    }

    // redirects the player controller to the gun script on the currently held gun
    public void Shoot()
    {
        if(m_currentGunScript != null)
            m_currentGunScript.Shoot();
    }
}
