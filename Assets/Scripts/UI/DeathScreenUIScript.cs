using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenUIScript : MonoBehaviour
{
    [SerializeField] private string m_firstLevelName;
    [SerializeField] private string m_mainMenuName;

    public void LoadGame()
    {
        SceneManager.LoadScene(m_firstLevelName);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(m_mainMenuName);
    }
}
