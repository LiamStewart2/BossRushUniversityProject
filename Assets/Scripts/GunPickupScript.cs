using UnityEngine;

public class GunPickupScript : MonoBehaviour
{
    [SerializeField] private GameObject m_gun;

    private void Awake()
    {
        Instantiate(m_gun.GetComponent<GunScript>().getSprite(), transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_gun != null && collision.tag == "Player")
        {
            collision.gameObject.GetComponent<GunManager>().EquipGun(m_gun);
            Destroy(gameObject);
        }
    }
}
