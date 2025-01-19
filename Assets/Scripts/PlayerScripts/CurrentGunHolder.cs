using UnityEngine;

public class CurrentGunHolder : MonoBehaviour
{
    public GameObject m_gun = null;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
