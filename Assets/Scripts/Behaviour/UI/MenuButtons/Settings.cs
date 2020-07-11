
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

    private const string k_LoadMenu = "BackToMainMenu";
    private const float k_StartVolume = 0.5f;

    public Dropdown m_ResolutionDropdown;

    [SerializeField] private GameObject m_PauseMenu;
    [SerializeField] private PlayerInput m_PlayerInput;

    private Resolution[] resolutions;
    private int m_StartSceneIndex = 0;
    private int m_CurrentSceneIndex = 1;
    private float m_Delay = 1f;

    private void Start()
    {
        resolutions = Screen.resolutions;
        if (m_ResolutionDropdown != null)
        {
            m_ResolutionDropdown.ClearOptions();

            List<string> options = new List<string>();


            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            m_ResolutionDropdown.AddOptions(options);
            m_ResolutionDropdown.value = currentResolutionIndex;
            m_ResolutionDropdown.RefreshShownValue();
            AudioListener.volume = k_StartVolume;
        }
    }

    public void VsyncToggle(bool toggled)
    {
        if (!toggled)
        {
            QualitySettings.vSyncCount = 0;
            print(toggled);
        }
        else if (toggled)
        {
            QualitySettings.vSyncCount = 1;
            print(toggled);
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void VolumeControl(float volume)
    {
        AudioListener.volume = volume;
    }
   
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    private void BackToMainMenu()
    {
        m_PauseMenu.SetActive(false);
        SceneManager.LoadScene(m_StartSceneIndex);
        SceneManager.UnloadSceneAsync(m_CurrentSceneIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Invoke(k_LoadMenu, m_Delay);
    }
}
