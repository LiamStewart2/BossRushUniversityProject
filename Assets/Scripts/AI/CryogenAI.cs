using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CryogenAI : EnemyScript
{
    [Header("AI Attributes")]
    [SerializeField] private float m_movementSpeed;
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

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Sprite m_FirstPhaseSprite;
    [SerializeField] private Sprite m_SecondPhaseSprite;
    [SerializeField] private Sprite m_ThirdPhaseSprite;

    [Header("Effects")]
    [SerializeField] private GameObject m_CryogenHitParticle;
    [SerializeField] private GameObject m_CryogenDeathParticle;
    [SerializeField] private AudioSource[] m_HitSounds;

    private Rigidbody2D m_rigidbody;
    private PHASE m_phase = PHASE.PHASE1;

    private float m_AttackTimer = 0.0f;
    private float m_ShieldTimer = 0.0f;
    private float m_ChargeTimer = 0.0f;

    private bool m_charging = false;
    private Vector2 m_chargeDirection = Vector2.zero;
    private float m_chargingTimer = 0.0f;
    
    private enum PHASE
    {
        PHASE1, PHASE2, PHASE3
    }

    private void Start()
    {

        m_rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer.sprite = m_FirstPhaseSprite;

        m_AttackTimer = m_FirstPhaseShootRate;
        m_ShieldTimer = m_FirstPhaseShieldRegenTime;
        m_ChargeTimer = m_SecondPhaseChargeTime;
    }

    public override void takeDamage(int damage)
    {
        base.takeDamage(damage);

        Instantiate(m_CryogenHitParticle, transform.position, transform.rotation, null);

        int hitSoundIndex = Random.Range(0, m_HitSounds.Length);
        m_HitSounds[hitSoundIndex].Play();

        switch (m_phase)
        {
            case PHASE.PHASE1:
                if (m_currentHealth < m_maxHealth * 0.7)
                {
                    m_phase = PHASE.PHASE2;
                    m_SpriteRenderer.sprite = m_SecondPhaseSprite;
                }
                break;

            case PHASE.PHASE2:
                if (m_currentHealth < m_maxHealth * 0.35)
                {
                    m_phase = PHASE.PHASE3;
                    m_SpriteRenderer.sprite = m_ThirdPhaseSprite;
                }
                break;
        }
    }

    public override void Die()
    {
        GameManager.instance.WonGame();
        Instantiate(m_CryogenDeathParticle, transform.position, transform.rotation, null);
        base.Die();
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

    private void FixedUpdate()
    {
        if (m_charging == false)
            MoveTowardsPlayer();
    }

    private void Phase1()
    {
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

            else
                m_rigidbody.linearVelocity = m_chargeDirection * m_SecondPhaseChargeVelocity;
        }

        else
        {
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
        if (m_charging)
        {
            m_chargingTimer -= Time.deltaTime;

            if (m_chargingTimer <= 0.0f)
                m_charging = false;

            else 
                m_rigidbody.linearVelocity = m_chargeDirection * m_SecondPhaseChargeVelocity;
        }

        else
        {
            m_AttackTimer -= Time.deltaTime;
            m_ChargeTimer -= Time.deltaTime;

            if (m_shield.m_IsShieldUp == false)
                m_ShieldTimer -= Time.deltaTime;

            if (m_AttackTimer <= 0.0f)
            {
                SpawnProjectiles(16);
                m_AttackTimer = m_ThirdPhaseShootRate;
            }

            if (m_ShieldTimer <= 0.0f)
            {
                m_shield.RespawnShield();
                m_ShieldTimer = m_FirstPhaseShieldRegenTime;
            }

            if (m_ChargeTimer <= 0.0f)
            {
                Charge();
                m_ChargeTimer = m_ThirdPhaseChargeTime;
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        m_rigidbody.linearVelocity = ((GameManager.instance.m_player.position + m_FirstPhaseOffsetPosition) - transform.position) * m_movementSpeed;
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
            if(m_charging)
                collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(m_ChargeDamage);
            else
                collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(m_damage);
        }
    }
}
