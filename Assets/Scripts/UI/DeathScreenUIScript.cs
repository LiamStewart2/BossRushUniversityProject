using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenUIScript : MonoBehaviour
{
    [SerializeField] private string m_firstLevelName;
    [SerializeField] private string m_mainMenuName;

    public void LoadGame()
    {
        GameObject.FindGameObjectWithTag("CurrentGun").GetComponent<CurrentGunHolder>().m_gun = null;
        SceneManager.LoadScene(m_firstLevelName);
    }
    public void LoadMainMenu()
    {
        GameObject.FindGameObjectWithTag("CurrentGun").GetComponent<CurrentGunHolder>().m_gun = null;
        SceneManager.LoadScene(m_mainMenuName);
    }
}
