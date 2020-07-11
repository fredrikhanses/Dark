using UnityEngine;

public class DialougeInteractable : MonoBehaviour, IInteract
{
    [SerializeField] private bool m_FaceInteractor;

    public Dialogue[] m_Dialogue;
    private int m_Index = 0;

    public void ImDoingMyThing()
    {
        GameManager.Instance.PlayerCharacter.m_DesiredMovement = Vector3.zero;
        if (m_FaceInteractor == true)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(-GameManager.Instance.CameraMovement.transform.forward);
        }
        StartDialogue();
    }

    private void StartDialogue()
    {
        UIController.Instance.DialogueBox.StartDialogue(m_Dialogue[m_Index]);
        m_Index++;
        if (m_Index >= m_Dialogue.Length)
        {
            m_Index = 0;
        }
    }
}
