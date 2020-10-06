using UnityEngine;

public class PuzzleSwitch : GateBoolean, IInteract
{
    private bool m_SwitchActive;
    [SerializeField] private Material m_OnActiveMaterial;
    [SerializeField] private MeshRenderer m_MeshRenderer;
    [SerializeField] private SkinnedMeshRenderer m_SkinnedMeshRenderer;

    public void ImDoingMyThing()
    {
        if (!m_SwitchActive)
        {
            Set(true);
            if (m_MeshRenderer && m_OnActiveMaterial)
            {
                m_MeshRenderer.material = m_OnActiveMaterial;
            }
            else if (m_SkinnedMeshRenderer && m_OnActiveMaterial)
            {
                m_SkinnedMeshRenderer.material = m_OnActiveMaterial;
            }
            else
            {
                throw new System.ArgumentNullException("Missing Material in: " + gameObject.name + " for PuzzleSwitch Script");
            }
        }
        else
        {
            Set(false);
            Set(true);
        }
        m_SwitchActive = true;
    }
}
