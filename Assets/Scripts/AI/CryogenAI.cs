using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CryogenAI : EnemyScript
{
    [Header("AI Attributes")]
    [SerializeField] private int m_damage;
    [SerializeField] private int m_ChargeDamage;

    [SerializeField] private float m_FirstPhaseShieldRegenTime;
    [SerializeField] private float m_FirstPhaseShootRate;
    [SerializeField] private Vector3 m_FirstPhaseOffsetPosition;

    [SerializeField] private float m_SecondPhaseChargeTime;
    [SerializeField] private float m_SecondPhaseChargeAttackRate;
    [SerializeField] private float m_SecondPhaseChargeVelocity;

    [SerializeField] private float m_ThirdPhaseShootRate;
    [SerializeField] private float m_ThirdPhaseChargeTime;

    [Header("AI Requirements")]
    [SerializeField] CryogenShield m_shield;


    [Header("Projectile Spawn Points")]
    [SerializeField] private Transform[] m_4_projectiles;
    [SerializeField] private Transform[] m_8_projectiles;
    [SerializeField] private GameObject m_projectile;

    private NavMeshAgent m_agent;
    private PHASE m_phase = PHASE.PHASE1;

    private float m_AttackTimer = 0.0f;
    private float m_ShieldTimer = 0.0f;
    private float m_ChargeTimer = 0.0f;
    
    private enum PHASE
    {
        PHASE1, PHASE2, PHASE3
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.updateRotation = false;
        m_agent.updateUpAxis = false;

        m_AttackTimer = m_FirstPhaseShootRate;
        m_ShieldTimer = m_FirstPhaseShieldRegenTime;
    }


    private void Update()
    {
        switch (m_phase)
        {
            case(PHASE.PHASE1):
            {
                Phase1();
                break;
            }
            case (PHASE.PHASE2):
            {
                Phase2();
                break;
            }
            case (PHASE.PHASE3):
            {
                Phase3();
                break;
            }
            default:
            {
                break;
            }
        }
    }


    private void Phase1()
    {
        Idle();

        m_AttackTimer -= Time.deltaTime;

        if(m_shield.m_IsShieldUp)
            m_ShieldTimer -= Time.deltaTime;

        if(m_AttackTimer <= 0.0f)
        {
            Spawn8Projectiles();
            m_AttackTimer = m_FirstPhaseShootRate;
        }

        if (m_ShieldTimer <= 0.0f)
        {
            m_shield.RespawnShield();
            m_ShieldTimer = m_FirstPhaseShieldRegenTime;
        }
    }

    private void Phase2()
    {

    }

    private void Phase3()
    {

    }

    private void Idle()
    {
        m_agent.SetDestination(GameManager.instance.m_player.position + m_FirstPhaseOffsetPosition);
    }

    private void Charge()
    {
        // At the start of charge find the vector towards the player
        // Then move in that vector direction for 1 second
        // during which rapidly shoot projects

    }

    // Projectile Spawns
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
