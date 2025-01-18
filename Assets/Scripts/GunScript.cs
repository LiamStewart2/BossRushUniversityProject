using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] private GameObject m_projectile;
    [SerializeField] private Transform m_barrelPosition;
    [SerializeField] private Transform m_sprite;
    [SerializeField] private float m_firerate;
    [SerializeField] private float m_bulletSpread;

    private float m_timer = 0.0f;
    private Vector2 m_gunSpritePosition;
    private Vector2 m_gunSpriteScale;

    private void Awake()
    {
        m_gunSpritePosition = m_sprite.localPosition;
        m_gunSpriteScale = m_sprite.localScale;
    }

    public void Update()
    {
        if (GameManager.instance.m_gameOver == false)
        {
            FaceMouse();
            if (m_timer > 0.0f)
                m_timer -= Time.deltaTime;
        }
    }

    public GameObject getSprite()
    {
        return m_sprite.gameObject;
    }

    public void Shoot()
    {
        if (m_timer <= 0.0f)
        {
            Quaternion bulletSpreadRotation = Quaternion.Euler(0, 0, m_barrelPosition.rotation.eulerAngles.z + Random.Range(-m_bulletSpread, m_bulletSpread));
            Instantiate(m_projectile, m_barrelPosition.position, bulletSpreadRotation, null);
            m_timer = 1.0f / m_firerate; 
        }
    }

    private void FaceMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(mousePosition.x < GameManager.instance.m_player.position.x)
        {
            m_sprite.localPosition = new Vector2(-m_gunSpritePosition.x, m_sprite.localPosition.y);
            m_sprite.localScale = new Vector2(m_gunSpriteScale.x, -m_gunSpriteScale.y);
            m_sprite.right = new Vector2(mousePosition.x - m_sprite.position.x, mousePosition.y - m_sprite.position.y);
            m_barrelPosition.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }

        else
        {
            m_sprite.localPosition = m_gunSpritePosition;
            m_sprite.localScale = m_gunSpriteScale;
            m_sprite.right = new Vector2(mousePosition.x - m_sprite.position.x, mousePosition.y - m_sprite.position.y);
            m_barrelPosition.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
    }
}
