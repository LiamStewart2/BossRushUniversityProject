using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Cave Entrance in the opening level
/// </summary>

public class CaveEntranceScript : MonoBehaviour
{
    [SerializeField] private string m_firstBossScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            SceneManager.LoadScene(m_firstBossScene);
    }
}
