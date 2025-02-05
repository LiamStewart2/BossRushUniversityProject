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
    [SerializeField] private float m_SecondPhaseChargeDuration;
    [SerializeField] private float m_SecondPhaseChargeAttackRate;
    [SerializeField] private float m_SecondPhaseChargeVelocity;

    [SerializeField] private float m_ThirdPhaseShootRate;
    [SerializeField] private float m_ThirdPhaseChargeTime;

    [Header("AI Requirements")]
    [SerializeField] CryogenShield m_shield;
    [SerializeField] private GameObject m_projectile;

    private Rigidbody2D m_rigidbody;
    private NavMeshAgent m_agent;
    private PHASE m_phase = PHASE.PHASE1;

    private float m_AttackTimer = 0.0f;
    private float m_ShieldTimer = 0.0f;
    private float m_ChargeTimer = 0.0f;

    private bool m_charging = true;
    private Vector2 m_chargeDirection = Vector2.zero;
    private float m_chargingTimer = 0.0f;
    
    private enum PHASE
    {
        PHASE1, PHASE2, PHASE3
    }

    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.updateRotation = false;
        m_agent.updateUpAxis = false;

        m_rigidbody = GetComponent<Rigidbody2D>();

        m_AttackTimer = m_FirstPhaseShootRate;
        m_ShieldTimer = m_FirstPhaseShieldRegenTime;
        m_ChargeTimer = m_SecondPhaseChargeTime;
    }

    public override void takeDamage(int damage)
    {
        base.takeDamage(damage);

        switch(m_phase)
        {
            case PHASE.PHASE1:
                if (m_currentHealth < m_maxHealth * 0.7)
                    m_phase = PHASE.PHASE2;
                break;

            case PHASE.PHASE2:
                if (m_currentHealth < m_maxHealth * 0.35)
                    m_phase = PHASE.PHASE3;
                break;
        }
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

        if(m_shield.m_IsShieldUp == false)
            m_ShieldTimer -= Time.deltaTime;

        if(m_AttackTimer <= 0.0f)
        {
            SpawnProjectiles(16);
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
        if(m_charging)
        {
            m_chargingTimer -= Time.deltaTime;
            
            if(m_chargingTimer <= 0.0f)
                m_charging = false;

            m_rigidbody.linearVelocity = m_chargeDirection * m_SecondPhaseChargeVelocity;
        }

        else
        {
            Idle();

            m_AttackTimer -= Time.deltaTime;
            m_ChargeTimer -= Time.deltaTime;

            if(m_shield.m_IsShieldUp == false)
                m_ShieldTimer -= Time.deltaTime;

            if(m_AttackTimer <= 0.0f)
            {
                SpawnProjectiles(16);
                m_AttackTimer = m_FirstPhaseShootRate;
            }

            if (m_ShieldTimer <= 0.0f)
            {
                m_shield.RespawnShield();
                m_ShieldTimer = m_FirstPhaseShieldRegenTime;
            }

            if (m_ChargeTimer <= 0.0f)
            {
                Charge();
                m_ChargeTimer = m_SecondPhaseChargeTime;
            }
        }
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
        m_charging = true;

        m_chargeDirection = GameManager.instance.m_player.position - transform.position;
        m_chargeDirection = m_chargeDirection.normalized;

        m_chargingTimer = m_SecondPhaseChargeDuration;
    }

    private void SpawnProjectiles(int n)
    {
        for(int i = 0; i < n; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, 360 / n * i);
            Instantiate(m_projectile, transform.position, rotation, null);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(m_damage);
        }
    }
}
