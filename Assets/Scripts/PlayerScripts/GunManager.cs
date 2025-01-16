using UnityEngine;

public class GunManager : MonoBehaviour
{
    private GameObject m_currentEquippedGun;
    private GunScript m_currentGunScript;

    public void EquipGun(GameObject newGun)
    {
        if (newGun.GetComponent<GunScript>() != null)
        {
            Destroy(m_currentEquippedGun);
            m_currentEquippedGun = Instantiate(newGun, transform);
            m_currentGunScript = m_currentEquippedGun.GetComponent<GunScript>();
        }
        else
        {
            Debug.LogError("[INVALID GUN] - " + newGun.name + " has no Gun Script");
        }
    }

    public void UnequipGun()
    {
        Destroy(m_currentEquippedGun);
        m_currentEquippedGun = null;
        m_currentGunScript = null;
    }

    public void Shoot()
    {
        if(m_currentGunScript != null)
            m_currentGunScript.Shoot();
    }
}
