using UnityEngine;

public class EventDialogue : MonoBehaviour, IEvent
{
    [SerializeField] Dialogue m_Dialogue;
    [SerializeField] Color m_TextColor;
    [SerializeField] Color m_BackgroundColor;

    public void Event()
    {
        UIController.Instance.StartBox.StartDialogue(m_Dialogue, false, m_TextColor, m_BackgroundColor);
    }
}
