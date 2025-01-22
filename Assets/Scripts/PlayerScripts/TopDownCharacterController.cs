using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using static UnityEngine.Timeline.DirectorControlPlayable;
using System;

public class TopDownCharacterController : MonoBehaviour
{
 
    private InputAction m_moveAction;
    private InputAction m_attackAction;
    private InputAction m_JumpAction;
    private InputAction m_PauseAction;
    private InputAction m_InteractAction;

    private Animator m_animator;
    private Rigidbody2D m_rigidbody;
    
    private Vector2 m_playerDirection;
   
    private Vector2 m_playerRollDirection;
    private bool rolling = false;

    private InteractableComponent m_interactable;

    [Header("Movement parameters")]
    [SerializeField] private float m_playerSpeed = 200f;
    [SerializeField] private float m_playerMaxSpeed = 1000f;

    [Header("Roll parameters")]
    [SerializeField] private float m_rollDuration = 0.3f;
    [SerializeField] private float m_rollSpeed = 500.0f;

    [Header("Gun Manager")]
    [SerializeField] private GunManager m_gunManager;


    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_attackAction = InputSystem.actions.FindAction("Attack");
        m_JumpAction = InputSystem.actions.FindAction("Jump");
        m_PauseAction = InputSystem.actions.FindAction("Pause");
        m_InteractAction = InputSystem.actions.FindAction("Interact");

        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.m_gameOver == false)
        {
            if (rolling == false)
            {
                float speed = m_playerSpeed > m_playerMaxSpeed ? m_playerMaxSpeed : m_playerSpeed;

                m_rigidbody.linearVelocity = m_playerDirection * (speed * Time.fixedDeltaTime);
            }
            else
            {
                m_rigidbody.linearVelocity = m_playerRollDirection * (m_rollSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            if(m_rigidbody != null)
            {
                Destroy(m_rigidbody); m_rigidbody = null;
            }
        }
    }
    
    void Update()
    {
        if (GameManager.instance.m_gameOver)
            return;

        if (m_PauseAction.triggered)
            GameManager.instance.PauseGame();

        m_playerDirection = m_moveAction.ReadValue<Vector2>();

        if (rolling == false)
        {
            m_animator.SetFloat("Speed", m_playerDirection.magnitude);
            m_animator.SetFloat("Horizontal", m_playerDirection.x);
            m_animator.SetFloat("Vertical", m_playerDirection.y);
        }

        if (m_interactable != null && m_InteractAction.IsPressed() && m_interactable.Interected() == false)
        {
            m_interactable.Interact();
            m_interactable = null;
            GameManager.instance.m_interactUI.SetActive(false);
        }

        if (rolling) return;

        if (m_JumpAction.triggered && m_playerDirection.magnitude != 0)
        {
            m_playerRollDirection = m_playerDirection;
            m_animator.SetFloat("Speed", m_playerDirection.magnitude);
            m_animator.SetFloat("Horizontal", m_playerRollDirection.x);
            m_animator.SetFloat("Vertical", m_playerRollDirection.y);
            StartCoroutine(Rolling());
        }

        if(m_attackAction.IsPressed())
        {
            m_gunManager.Shoot();
        }
    }

    IEnumerator Rolling()
    {
        rolling = true;
        m_animator.SetBool("Rolling", true);

        yield return new WaitForSeconds(m_rollDuration);

        m_animator.SetBool("Rolling", false);
        rolling = false;

        m_animator.SetFloat("Speed", m_playerDirection.magnitude);
        m_animator.SetFloat("Horizontal", m_playerDirection.x);
        m_animator.SetFloat("Vertical", m_playerDirection.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            m_interactable = collision.gameObject.GetComponent<InteractableComponent>();
            if(m_interactable != null && m_interactable.Interected() == false)
            {
                GameManager.instance.m_interactUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Interactable")
        {
            m_interactable = null;
            GameManager.instance.m_interactUI.SetActive(false);
        }
    }
}
