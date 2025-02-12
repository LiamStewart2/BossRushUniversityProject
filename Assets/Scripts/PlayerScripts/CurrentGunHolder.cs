using UnityEngine;

/// <summary>
/// Stores the currently held gun between scenes
/// </summary>

public class CurrentGunHolder : MonoBehaviour
{
    public GameObject m_gun = null;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
