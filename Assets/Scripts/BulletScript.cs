using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D m_rb;
    [SerializeField] private float m_bulletSpeed;
    [SerializeField] private int m_bulletDamage;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void FixedUpdate()
    {
        m_rb.linearVelocity = transform.up * Time.deltaTime * m_bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().takeDamage(m_bulletDamage);

            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "BlockProjectile")
            Destroy(gameObject);
    }
}
