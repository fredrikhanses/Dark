using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TakeColor : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_MeshRenderer;
    private Color m_Color;

    private void Start()
    {
        if (m_MeshRenderer == null)
        {
            m_MeshRenderer = GetComponent<MeshRenderer>();
        }
        if (m_MeshRenderer != null)
        {
            m_Color = m_MeshRenderer.material.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
        if (meshRenderer)
        {
            meshRenderer.material.color = m_Color;
        }
    }
}
