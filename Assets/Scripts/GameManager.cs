using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_deathScreen;
    [SerializeField] private GameObject m_winScreen;
    [SerializeField] private GameObject m_pauseMenu;

    public static GameManager instance;

    public GameObject m_interactUI;
    public Transform m_player;
    public HealthbarScript m_healthbarScript;
    public bool m_gameOver = false;
    public bool m_paused = false;

    public void PauseGame()
    {
        m_pauseMenu.SetActive(true);
        m_paused = true;
        Time.timeScale = 0.0f;
    }
    public void ContinueGame()
    {
        m_pauseMenu.SetActive(false);
        m_paused = false;
        Time.timeScale = 1.0f;
    }


    public void LostGame()
    {
        m_gameOver = true;
        m_deathScreen.SetActive(true);
    }
    public void WonGame()
    {
        m_gameOver = true;
        m_winScreen.SetActive(true);
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
