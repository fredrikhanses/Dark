using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public UIDialogue DialogueBox;
    public UIStartDialogue StartBox;
    public UIDash m_UIDash;
    public UIInteractPrompt m_Interact;

    [NonSerialized] public bool InputConformation;
    [NonSerialized] public bool UIInput;

    public GameObject m_Canvas;
    public GameObject m_MainMenu;
    public GameObject m_PauseMenu;
    public GameObject m_Settings;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (DialogueBox != null)
        {
            DialogueBox.gameObject.SetActive(false);
        }
        if (StartBox != null)
        {
            StartBox.gameObject.SetActive(false);
        }
        if (m_UIDash != null)
        {
            m_UIDash.gameObject.SetActive(false);
        }
        if (m_PauseMenu != null)
        {
            m_PauseMenu.SetActive(false);
        }
        if (m_Settings != null)
        {
            m_Settings.SetActive(false);
        }
        if (m_Interact != null)
        {
            m_Interact.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        GameManager.Instance.DontDestroyOnLoadList.Add(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == GameManager.Instance.WorldStateData.OverWorld)
        {
            if (m_MainMenu != null)
            {
                m_MainMenu.SetActive(false);
            }
            if (m_UIDash != null)
            {
                m_UIDash.gameObject.SetActive(true);
            }
        }
    }

    public bool IsUIInput()
    {
        return UIInput;
    }

    public bool UIInputConformation()
    {
        return InputConformation;
    }
}
