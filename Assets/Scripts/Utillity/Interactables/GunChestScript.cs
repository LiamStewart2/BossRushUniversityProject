using UnityEngine;

/// <summary>
/// Spawns a random gun from the waepon pool when interacted with
/// </summary>

public class GunChestScript : MonoBehaviour
{
    [SerializeField] private GameObject[] m_weaponPool;
    [SerializeField] private Animator m_chessAnimator;
    [SerializeField] private Transform m_weaponPickupSpawnPoint;

    public void OpenChest()
    {
        m_chessAnimator.Play("Opening");
    }

    public void SpawnGun()
    {
        int weaponIndex = Random.Range(0, m_weaponPool.Length);
        Instantiate(m_weaponPool[weaponIndex], m_weaponPickupSpawnPoint);
    }
}
