using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenuUIScript : MonoBehaviour
{
    [SerializeField] private string m_mainMenuString;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(m_mainMenuString);
        Time.timeScale = 1.0f;
    }
}
