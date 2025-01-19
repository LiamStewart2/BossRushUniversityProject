using UnityEngine;

public class ChessChildScript : MonoBehaviour
{
    [SerializeField] private GunChestScript parent;

    public void SpawnGun()
    {
        parent.SpawnGun();
    }
}
