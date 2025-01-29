using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    [Header("Projectile settings")]
    [SerializeField] float m_movementSpeed;
    [SerializeField] float m_lifetime;
    [SerializeField] int m_damage;

    private void Update()
    {
        transform.position += transform.up * m_movementSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(m_damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "BlockProjectile")
            Destroy(gameObject);
    }
}
