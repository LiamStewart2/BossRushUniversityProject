using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_deathScreen;

    public static GameManager instance;

    public Transform m_player;
    public HealthbarScript m_healthbarScript;
    public bool m_gameOver = false;


    public void LostGame()
    {
        m_gameOver = true;
        m_deathScreen.SetActive(true);
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
