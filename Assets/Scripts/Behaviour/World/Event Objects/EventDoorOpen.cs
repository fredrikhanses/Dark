using UnityEngine;

public class EventDoorOpen : MonoBehaviour, IEvent
{
    [SerializeField] private Material m_ActivatedMaterial;
    [SerializeField] private Material m_DeactivatedMaterial;

    [SerializeField] private DoorOpen m_MyDoor;
    
    private MeshRenderer m_MeshRenderer;

    private bool m_Activated;
    public bool Activated
    {
        get => m_Activated;
        private set => m_Activated = value;
    }

    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_MeshRenderer.material = m_DeactivatedMaterial;
    }

    public void Event()
    {
        if (!m_Activated)
        {
            m_MeshRenderer.material = m_ActivatedMaterial;
            Activated = true;
            m_MyDoor.DeductActivater(m_Activated);
            Debug.Log("Activated");
        } else if (m_Activated)
        {
            m_MeshRenderer.material = m_DeactivatedMaterial;
            Activated = false;
            m_MyDoor.DeductActivater(m_Activated);
            Debug.Log("Deactivated");
        }
        else
        {
            Debug.Log("Already activated");
        }
    }
}
