using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color m_flashColor = Color.white;
    [SerializeField] private float m_flashTime = 0.2f;

    private Material m_flashMaterial;

    private Coroutine m_flashCoroutine;

    private void Awake()
    {
        m_flashMaterial = GetComponent<SpriteRenderer>().material;
        m_flashMaterial.color = m_flashColor;
    }

    public void Flash()
    {
        m_flashCoroutine = StartCoroutine(DamageFlashRoutine());
    }    

    private IEnumerator DamageFlashRoutine()
    {
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;

        while(elapsedTime < m_flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / m_flashTime));
            m_flashMaterial.SetFloat("_FlashAmount", currentFlashAmount);

            yield return null;
        }
    }
}
