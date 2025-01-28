using UnityEngine;
using UnityEngine.AI;

public class CryogenAI : EnemyScript
{
    [Header("Bat Attributes")]
    [SerializeField] private int m_damage;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_bumpForce;
    [SerializeField] private float m_acceleration;

    private Rigidbody2D rb;
    private NavMeshAgent m_agent;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.updateRotation = false;
        m_agent.updateUpAxis = false;
    }

    private void FixedUpdate()
    {
        Vector3 cv = Vector3.zero;
        Vector3 targetVelocity = (GameManager.instance.m_player.position - transform.position).normalized * m_speed * Time.deltaTime;
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref cv, m_acceleration);

        m_agent.SetDestination(GameManager.instance.m_player.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(m_damage);
            rb.AddForce(-(GameManager.instance.m_player.position - transform.position) * m_bumpForce);
        }
    }
}
