using UnityEngine;

[RequireComponent(typeof(SoundManager))]
public class InteractPlaySound : MonoBehaviour, IInteract
{
    SoundManager m_Sound;
    [SerializeField] string m_EventSoundName;

    public void ImDoingMyThing()
    {
        m_Sound.PlaySound(m_EventSoundName);
    }

    private void Awake()
    {
        m_Sound = GetComponent<SoundManager>();
    }
}
