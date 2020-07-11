using UnityEngine;

public class DisableLight : MonoBehaviour, IEvent
{
    [SerializeField] private Light m_Light;

    void Start()
    {
        if (m_Light == null)
        {
            m_Light = FindObjectOfType<Light>();
        }
    }

    public void Event()
    {
        if (m_Light != null)
        {
            m_Light.gameObject.SetActive(false);
        }
    }
}
