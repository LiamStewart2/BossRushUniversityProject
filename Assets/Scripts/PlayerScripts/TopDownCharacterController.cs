using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class TopDownCharacterController : MonoBehaviour
{
 
    private InputAction m_moveAction;
    private InputAction m_attackAction;
    private InputAction m_JumpAction;
    private InputAction m_PauseAction;

    private Animator m_animator;
    private Rigidbody2D m_rigidbody;
    
    private Vector2 m_playerDirection;
   
    private Vector2 m_playerRollDirection;
    private bool rolling = false;

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
        if (GameManager.instance.m_gameOver == false)
        {
            if(m_PauseAction.IsPressed())
                GameManager.instance.PauseGame();

            m_playerDirection = m_moveAction.ReadValue<Vector2>();

            if (rolling == false)
                m_animator.SetFloat("Speed", m_playerDirection.magnitude);

            if (m_JumpAction.triggered && rolling == false)
            {
                m_animator.SetBool("Rolling", true);
                m_playerRollDirection = m_playerDirection;
                rolling = true;

                m_animator.SetFloat("Speed", m_playerDirection.magnitude);
                StartCoroutine(Rolling());
            }

            if (m_playerDirection.magnitude > 0 && rolling == false)
            {
                m_animator.SetFloat("Horizontal", m_playerDirection.x);
                m_animator.SetFloat("Vertical", m_playerDirection.y);
            }

            if (m_attackAction.IsPressed())
            {
                m_gunManager.Shoot();
            }
        }
    }

    IEnumerator Rolling()
    {
        yield return new WaitForSeconds(m_rollDuration);
        m_animator.SetBool("Rolling", false);
        rolling = false;
    }
}
