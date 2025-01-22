using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenuUIScript : MonoBehaviour
{
    [SerializeField] private string m_mainMenuString;

    public void LoadMainMenu()
    {
        GameObject.FindGameObjectWithTag("CurrentGun").GetComponent<CurrentGunHolder>().m_gun = null;
        SceneManager.LoadScene(m_mainMenuString);
        Time.timeScale = 1.0f;
    }
}
