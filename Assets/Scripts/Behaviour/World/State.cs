using UnityEngine;
using UnityEngine.Assertions;

public class State : MonoBehaviour, IEvent
{
    [SerializeField] private bool m_SpiritState = false;
    [SerializeField] private Transform m_SpiritTransform;
    [SerializeField] private Transform m_NormalTransform;

    private Quaternion m_InitialRotation;
    private Quaternion m_ChangedRotation;

    public bool SpiritState { get => m_SpiritState; set => m_SpiritState = value; }

    public void Event()
    {
        SpiritState = !SpiritState;
        if (SpiritState == true)
        {
            //transform.position = m_SpiritTransform.position;
            transform.rotation = m_ChangedRotation;
            //transform.localScale = m_SpiritTransform.localScale;
        }
        if (SpiritState == false)
        {
            //transform.position = m_NormalTransform.position;
            transform.rotation = m_InitialRotation;
            //transform.localScale = m_NormalTransform.localScale;
        }
    }

    private void Start()
    {
        Assert.IsNotNull(m_SpiritTransform);
        m_ChangedRotation = Quaternion.Euler(m_SpiritTransform.rotation.eulerAngles.x, m_SpiritTransform.rotation.eulerAngles.y, m_SpiritTransform.rotation.eulerAngles.z);
        if (m_NormalTransform == null)
        {
            m_NormalTransform = transform;
        }
        Assert.IsNotNull(m_NormalTransform);
        m_InitialRotation = Quaternion.Euler(m_NormalTransform.rotation.eulerAngles.x, m_NormalTransform.rotation.eulerAngles.y, m_NormalTransform.rotation.eulerAngles.z);
    }
}
