using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Interactable component - allows Gameobjects to have interact functionality with the player
/// atatch the function to interact with the m_interact event
/// will only be triggered once
/// </summary>

public class InteractableComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent m_interact;
    
    public string m_interactKeyword;

    private bool m_interacted = false;
    public bool Interected() { return m_interacted;}

    private void Awake()
    {
        gameObject.tag = "Interactable";
    }

    public void Interact()
    {
        m_interact.Invoke();
        m_interacted = true;
    }
}
