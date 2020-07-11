using UnityEngine;

public class Master_Menu : MonoBehaviour
{
    private Animator m_CanvasAnimator;

    [SerializeField] private GameObject m_SplashScreen;

    private const string k_FadeMain = "FadeInMenu";
    private const string k_MainToCredits = "MainToCredits";
    private const string k_CreditsToMain = "CreditsToMain";
    private const string k_MainToSettings = "Settings";
    private const string k_SettingsToMain = "SettingsToMainMenu";
    private const string k_StartGame = "StartGame";
    private const string k_ResumeGame = "ResumeGame";
    private const string k_SplashScreen = "SplashScreen";

    private void Awake()
    {
        m_CanvasAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        //m_CanvasAnimator.SetBool(k_FadeMain, true);
        m_CanvasAnimator.SetBool(k_SplashScreen, true);
    }
    public void MainToCredits()
    {
        m_CanvasAnimator.SetBool(k_MainToCredits, false);
    }

    public void DisableMainMenu()
    {
        m_CanvasAnimator.SetBool(k_FadeMain, false);
    }

    public void DisableCredits()
    {
        m_CanvasAnimator.SetBool(k_CreditsToMain, false);
    }

    public void DisableSettings()
    {
        m_CanvasAnimator.SetBool(k_MainToSettings, false);
    }

    public void SettingsToMenu()
    {
        m_CanvasAnimator.SetBool(k_SettingsToMain, false);
    }

    public void EnableMainMenu()
    {
        m_CanvasAnimator.SetBool(k_FadeMain, true);
    }

    public void DisableStartGame()
    {
        m_CanvasAnimator.SetBool(k_StartGame, false);
    }

    public void DisableResumeGame()
    {
        m_CanvasAnimator.SetBool(k_ResumeGame, false);
    }

    public void DisableSplashScreen()
    {
        m_CanvasAnimator.SetBool(k_SplashScreen, false);
    }

    public void DisableAllButMain()
    {
        m_CanvasAnimator.SetBool(k_MainToCredits, false);
        m_CanvasAnimator.SetBool(k_CreditsToMain, false);
        m_CanvasAnimator.SetBool(k_MainToSettings, false);
        m_CanvasAnimator.SetBool(k_SettingsToMain, false);
    }

    public void DisableAllMenu()
    {
        m_CanvasAnimator.SetBool(k_MainToCredits, false);
        m_CanvasAnimator.SetBool(k_CreditsToMain, false);
        m_CanvasAnimator.SetBool(k_MainToSettings, false);
        m_CanvasAnimator.SetBool(k_SettingsToMain, false);
        m_CanvasAnimator.SetBool(k_FadeMain, false);
        m_CanvasAnimator.SetBool(k_StartGame, false);
    }

    public void DisableAllButSettings()
    {
        m_CanvasAnimator.SetBool(k_MainToCredits, false);
        m_CanvasAnimator.SetBool(k_CreditsToMain, false);
        m_CanvasAnimator.SetBool(k_SettingsToMain, false);
        m_CanvasAnimator.SetBool(k_FadeMain, false);
        m_CanvasAnimator.SetBool(k_StartGame, false);
    }

    public void DisableAllButCredits()
    {
        m_CanvasAnimator.SetBool(k_CreditsToMain, false);
        m_CanvasAnimator.SetBool(k_MainToSettings, false);
        m_CanvasAnimator.SetBool(k_SettingsToMain, false);
        m_CanvasAnimator.SetBool(k_FadeMain, false);
        m_CanvasAnimator.SetBool(k_StartGame, false);
    }
}
