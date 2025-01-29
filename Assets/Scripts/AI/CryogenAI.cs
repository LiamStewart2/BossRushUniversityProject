using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class CryogenAI : EnemyScript
{
    [Header("AI Attributes")]
    [SerializeField] private int m_damage;

    [Header("Projectile Spawn Points")]
    [SerializeField] private Transform[] m_4_projectiles;
    [SerializeField] private Transform[] m_8_projectiles;
    [SerializeField] private GameObject m_projectile; 


    private NavMeshAgent m_agent;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.updateRotation = false;
        m_agent.updateUpAxis = false;
    }


    private void Update()
    {
        m_agent.SetDestination(GameManager.instance.m_player.position);
        Spawn8Projectiles();
    }

    private void Spawn4Projectiles()
    {
        for (int i = 0; i < m_4_projectiles.Length; i++)
            Instantiate(m_projectile, m_4_projectiles[i].position, m_4_projectiles[i].rotation, null);
    }

    private void Spawn8Projectiles()
    {
        for (int i = 0; i < m_8_projectiles.Length; i++)
            Instantiate(m_projectile, m_8_projectiles[i].position, m_8_projectiles[i].rotation, null);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(m_damage);
        }
    }
}
