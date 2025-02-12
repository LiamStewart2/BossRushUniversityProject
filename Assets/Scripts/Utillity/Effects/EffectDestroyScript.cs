using UnityEngine;

/// <summary>
/// Cleanup Particle Effects
/// </summary>

public class EffectDestroyScript : MonoBehaviour
{
    [SerializeField] private float lifetime;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
