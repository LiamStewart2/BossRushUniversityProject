using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonHoverEffects : MonoBehaviour
{
    [SerializeField] private float m_scale;
    [SerializeField] private AudioClip hoverSFX;

    private RectTransform m_buttonRect;
    private Rect m_hoverRect;
    private AudioSource m_audioSource;

    private void Awake()
    {
        m_buttonRect = GetComponent<RectTransform>();
        m_hoverRect = m_buttonRect.rect;

        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.clip = hoverSFX;
    }

    public void OnHover()
    {
        m_buttonRect.localScale = new Vector3(m_scale, m_scale, m_scale);
        m_audioSource.Play();
    }

    public void EndHover()
    {
        m_buttonRect.localScale = Vector3.one;
    }
}
