using UnityEngine;

public class ChangingStone : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_MeshRenderer;
    [SerializeField] private MeshCollider m_MeshCollider;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameManager.Instance.DontDestroyOnLoadList.Add(gameObject);
        if (m_MeshRenderer == null)
        {
            m_MeshRenderer = GetComponent<MeshRenderer>();
        }
        if (m_MeshCollider == null)
        {
            m_MeshCollider = GetComponent<MeshCollider>();
        }
    }

    public void SetEnable(bool enable)
    {
        if (m_MeshRenderer != null && m_MeshCollider != null)
        {
            m_MeshRenderer.enabled = enable;
            m_MeshCollider.enabled = enable;
        }
    }
}

