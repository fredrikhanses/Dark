using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(SoundManager))]
public class TreeBreakable : Breakable
{
    [SerializeField] private SoundManager m_Sound;
    [SerializeField] private Dialogue dialogueOnBreak;
    [SerializeField] private DisableLight m_DisableLight;
    [SerializeField] private LoadSceneTrigger m_LoadSceneTrigger;
    [Space]
    [Header("Colors")]
    [SerializeField] private Color m_TextColor;
    [SerializeField] private Color m_BackgroundColor;

    private const string k_TreeBreak = "Tree Break";

    private void Awake()
    {
        if (m_Sound == null)
        {
            m_Sound = GetComponent<SoundManager>();
        }
    }

    private void Start()
    {
        if (dialogueOnBreak == null)
        {
            dialogueOnBreak = FindObjectOfType<Dialogue>();
        }
        if (m_DisableLight == null)
        {
            m_DisableLight = FindObjectOfType<DisableLight>();
        }
        if (m_LoadSceneTrigger == null)
        {
            m_LoadSceneTrigger = FindObjectOfType<LoadSceneTrigger>();
        }
    }

    public override void OnBreak()
    {
        //if (m_Sound != null)
        //{
        //    m_Sound.PlaySound(k_TreeBreak);
        //}

        if (dialogueOnBreak != null)
        {
            UIController.Instance.StartBox.StartDialogue(dialogueOnBreak, true, m_BackgroundColor, m_TextColor);
        }
        ChangeWorldState();
        CreateVisualEffects();
    }

    void CreateVisualEffects()
    {

    }

    void ChangeWorldState()
    {
        GameManager.Instance.CameraMovement.m_Static = true;
        GameManager.Instance.CameraMovement.transform.position = GameManager.Instance.m_OriginalCameraPosition;
        GameManager.Instance.CameraMovement.transform.rotation = GameManager.Instance.m_OriginalCameraRotation;
        GameManager.Instance.ShrineCompleted();
        if (m_DisableLight != null)
        {
            m_DisableLight.Event();
        }
        if (m_LoadSceneTrigger != null)
        {
            m_LoadSceneTrigger.Event();
        }
    }
}
