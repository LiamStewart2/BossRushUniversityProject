using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonHoverEffects : MonoBehaviour
{
    [SerializeField] private float m_scale;
    [SerializeField] private AudioClip hoverSFX;

    [SerializeField] private bool m_changeColourOnHover;
    [SerializeField] private Color m_hoverColour;

    private RectTransform m_buttonRect;
    private Rect m_hoverRect;
    private AudioSource m_audioSource;
    private Image m_image;
    private Color m_normalColour;

    private void Awake()
    {
        m_buttonRect = GetComponent<RectTransform>();
        m_hoverRect = m_buttonRect.rect;

        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.clip = hoverSFX;

        m_image = GetComponent<Image>();
        m_normalColour = m_image.color;
    }

    private void OnEnable()
    {
        EndHover();
    }

    public void OnHover()
    {
        m_buttonRect.localScale = new Vector3(m_scale, m_scale, m_scale);
        m_audioSource.Play();

        if(m_changeColourOnHover)
            m_image.color = m_hoverColour;
    }

    public void EndHover()
    {
        m_buttonRect.localScale = Vector3.one;
        if (m_changeColourOnHover)
            m_image.color = m_normalColour;
    }
}
