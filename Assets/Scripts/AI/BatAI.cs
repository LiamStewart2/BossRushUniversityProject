using UnityEngine;

public class BatAI : EnemyScript
{
    [Header("Bat Attributes")]
    [SerializeField] private int m_damage;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_bumpForce;
    [SerializeField] private float m_acceleration;

    private Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        Vector3 cv = Vector3.zero;
        Vector3 targetVelocity = (GameManager.instance.m_player.position - transform.position).normalized * m_speed * Time.deltaTime;
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref cv, m_acceleration);

        if (rb.linearVelocityX > 0)
            transform.localScale = new Vector2(-1, 1);
        else
            transform.localScale = new Vector2(1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(m_damage);
            rb.AddForce(-(GameManager.instance.m_player.position - transform.position) * m_bumpForce);
        }
    }
}
