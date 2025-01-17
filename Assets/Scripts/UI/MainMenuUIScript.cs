using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIScript : MonoBehaviour
{
    [SerializeField] private string m_firstLevelName;

    public void PlayGame()
    {
        SceneManager.LoadScene(m_firstLevelName);
    }
}
