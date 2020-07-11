using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private float m_KillDelay = 2f;
    [SerializeField] private PlayerCharacter m_Player;
    [SerializeField] private Material m_ShadowMaterial;

    private SoundManager m_PlayerSoundManager;
    private Material[] m_PlayerMaterials;
    private Material[] m_ShadowMaterials;
    private const string k_ResetPlayer = "ResetPlayer";
    private bool m_ResetingPlayer = false;

    private void Start()
    {
        if(m_Player == null)
        {
            m_Player = FindObjectOfType<PlayerCharacter>();
        }
        if(m_Player != null)
        {
            m_PlayerSoundManager = m_Player.GetComponent<SoundManager>();
            m_PlayerMaterials = m_Player.MeshRenderer.materials;
            for (int i = 0; i < m_PlayerMaterials.Length; i++)
            {
                m_PlayerMaterials[i] = m_Player.MeshRenderer.materials[i];
            }
            m_ShadowMaterials = new Material[m_PlayerMaterials.Length];
        }
        if(m_ShadowMaterial == null)
        {
            m_ShadowMaterial = GetComponent<MeshRenderer>().material;
            for (int i = 0; i < m_ShadowMaterials.Length; i++)
            {
                m_ShadowMaterials[i] = m_ShadowMaterial;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject == m_Player.gameObject && m_ResetingPlayer == false)
        {
            m_ResetingPlayer = true;
            m_PlayerSoundManager.PlaySound(k_ResetPlayer);
            Invoke(k_ResetPlayer, m_KillDelay);
            //Apply shadow materials
            m_Player.MeshRenderer.materials = m_ShadowMaterials;
        }
    }

    private void ResetPlayer()
    {
        GameManager.Instance.RespawnPlayer();
        //Apply player materials
        m_Player.MeshRenderer.materials = m_PlayerMaterials;
        m_ResetingPlayer = false;
    }
}
