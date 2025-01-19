using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    [SerializeField] private Slider m_slider;

    public void SetHealthByPercentage(float percentage)
    {
        gameObject.SetActive(true);
        m_slider.value = Mathf.Min(Mathf.Max(0, percentage), 1);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
