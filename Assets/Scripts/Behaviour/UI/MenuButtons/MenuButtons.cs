using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject m_Settings;
    [SerializeField] private GameObject m_MainMenu;
    [SerializeField] private GameObject m_PauseMenu;
    [SerializeField] private GameObject m_Credits;
    [SerializeField] private GameObject m_ConfirmationRestart;
    [SerializeField] private SoundManager m_SoundManager;
    [SerializeField] private List<Button> m_MainMenuButtons = new List<Button>();
    [SerializeField] private Text m_BackText;
    [SerializeField] private List<Animator> m_LogoTest = new List<Animator>();
    [Header("Intro Sequence")]
    [SerializeField] private Color m_TextColor;
    [SerializeField] private Color m_BackgroundColor;
    [SerializeField] private Dialogue m_IntroSequence;

    private float m_Delay = 2f;
    private const string k_LoadGame = "LoadGame";
    private const string k_Quit = "Quit";
    private const string k_Start = "Start";
    private const string k_Select = "Select";
    private const string k_Back = "Back";
    private const string k_PlayIntroSequence = "PlayIntroSequence";
    private const string k_Pause = "Pause";

    private Animator m_Animator;
    private Animator m_StartGameFade;
    private GameObject m_FadeInObject;
    private bool m_IsPaused;
    private bool m_InSettings;
    private bool m_IsInDialogue;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        m_FadeInObject = FindObjectOfType<FadeDummy>().gameObject;
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.DontDestroyOnLoadList.Add(gameObject);
        m_IsInDialogue = UIController.Instance.IsUIInput();
    }

    private void Update()
    {
        if (Input.GetButtonDown(k_Pause) && !UIController.Instance.IsUIInput())
        {
            if (m_PauseMenu != null && SceneManager.GetActiveScene().buildIndex != 0)
            {
                if (!m_PauseMenu.gameObject.activeSelf)
                {
                    if (!m_InSettings)
                    {
                        if (m_Settings.gameObject.activeSelf)
                        {
                            m_Settings.gameObject.SetActive(false);
                        }
                        m_PauseMenu.gameObject.SetActive(true);
                        m_IsPaused = true;
                        DisablePlayer();
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                    }
                }
                else
                {
                    if (!m_InSettings)
                    {
                        m_PauseMenu.gameObject.SetActive(false);
                        m_IsPaused = false;
                        EnablePlayer();
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }
            }
        }
    }

    public void StartGame()
    {
        //open correct scene
        if (m_SoundManager != null)
        {
            m_SoundManager.PlaySound(k_Start);
        }
        m_Animator.SetBool("StartGame", true);
        for(int i = 0; i < m_MainMenuButtons.Count; i++)
        {
            m_MainMenuButtons[i].interactable = false;
        }
        Invoke(k_PlayIntroSequence, m_Delay);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void PlayIntroSequence()
    {
        UIController.Instance.StartBox.StartDialogue(m_IntroSequence, true, m_BackgroundColor, m_TextColor);
        LoadGame();
    }

    private void LoadGame()
    {
        SceneManager.LoadSceneAsync(GameManager.Instance.WorldStateData.OverWorld);
    }

    public void Settings()
    {
        //settings menu
        if(m_Settings != null)
        {
            if (m_SoundManager != null)
            {
                m_SoundManager.PlaySound(k_Select);
            }
            m_Animator.SetBool("Settings", true);
        }
    }

    public void QuitGame()
    {
        if (m_SoundManager != null)
        {
            m_SoundManager.PlaySound(k_Quit);
        }
        Invoke(k_Quit, m_Delay);
    }

    private void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        if(m_MainMenu != null)
        {
            if (m_SoundManager != null)
            {
                m_SoundManager.PlaySound(k_Back);
            }
            if(GameManager.Instance.WorldStateData.CurrentLevel == GameManager.Instance.WorldStateData.StartMenu)
            {
                m_Animator.SetBool("SettingsToMainMenu", true);
            } 
            else
            {
                if (m_SoundManager != null)
                {
                    m_SoundManager.PlaySound(k_Select);
                }
                m_Animator.SetBool("ResumeGame", true);
                m_PauseMenu.SetActive(true);
                m_InSettings = false;
            }
        }
    } 

    public void PauseSettings()
    {
        if(m_Settings != null)
        {
            if (m_SoundManager != null)
            {
               m_SoundManager.PlaySound(k_Select);
            }
            m_BackText.text = k_Back;
            m_Animator.SetBool("Settings", true);
            m_PauseMenu.SetActive(false);
            m_InSettings = true;
        }
    }

    public void Credits()
    {
        if(m_Credits != null)
        {
            if (m_SoundManager != null)
            {
                m_SoundManager.PlaySound(k_Select);
            }
            m_Animator.SetBool("MainToCredits", true);
        }
    }

    public void BackFromCredits()
    {
        if(m_MainMenu != null)
        {
            if (m_SoundManager != null)
            {
                m_SoundManager.PlaySound(k_Back);
            }
            m_Animator.SetBool("CreditsToMain", true);
        }
    }

    public void ResumeGame()
    {
        if(m_SoundManager != null)
        {
            m_SoundManager.PlaySound(k_Select);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EnablePlayer();
        m_IsPaused = false;
        m_PauseMenu.SetActive(false);
    }

    private void EnablePlayer()
    {
        GameManager.Instance.PlayerInput.enabled = true;
        GameManager.Instance.PlayerRigidbody.useGravity = true;
        GameManager.Instance.PlayerCharacter.OnDashEnd();
        Time.timeScale = 1f;
    }

    private void DisablePlayer()
    {
        GameManager.Instance.PlayerInput.enabled = false;
        GameManager.Instance.PlayerRigidbody.useGravity = false;
        GameManager.Instance.PlayerRigidbody.velocity = Vector3.zero;
        GameManager.Instance.PlayerCharacter.m_DesiredMovement = Vector3.zero;
    }

    public void RestartLevel()
    {
        
        GameManager.Instance.ResetGame();
        Cursor.lockState = CursorLockMode.Locked;
        m_PauseMenu.gameObject.SetActive(false);
        m_ConfirmationRestart.SetActive(false);
        m_InSettings = false;
        EnablePlayer();
        print("Restarting Game");
    }

    public void ConfirmationRestart(GameObject confirmationWindow)
    {
        if (m_SoundManager != null)
        {
            m_SoundManager.PlaySound(k_Select);
        }
        confirmationWindow.SetActive(true);
        m_PauseMenu.SetActive(false);
        m_InSettings = true;
    }

    public void BackToPauseMenu(GameObject confirmationWindow)
    {
        if (m_SoundManager != null)
        {
            m_SoundManager.PlaySound(k_Select);
        }
        confirmationWindow.SetActive(false);
        m_PauseMenu.SetActive(true);
        m_InSettings = false;
    }

    public void BackToMainMenu()
    {
        
        GameManager.Instance.ResetToMainMenu();
        print("Going back to main menu, hold on.");
        m_InSettings = false;
    }
}
