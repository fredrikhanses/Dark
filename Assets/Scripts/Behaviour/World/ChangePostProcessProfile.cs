using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;

public class ChangePostProcessProfile : MonoBehaviour, IEvent
{
    [SerializeField] private Volume m_PostProcessVolume;
    [SerializeField] private VolumeProfile m_PostProcessProfile;
    [SerializeField, Range(0f, 10f)] private float m_ChangeDelay = 1f;

    private const string k_ChangeProfile = "ChangeProfile";

    private void Start()
    {
        if (m_PostProcessVolume == null)
        {
            m_PostProcessVolume = FindObjectOfType<Volume>();
        }
        Assert.IsNotNull(m_PostProcessVolume);
        Assert.IsNotNull(m_PostProcessProfile);
    }

    public void Event()
    {
        Invoke(k_ChangeProfile, m_ChangeDelay);
    }

    private void ChangeProfile()
    {
        m_PostProcessVolume.profile = m_PostProcessProfile;
    }
}
