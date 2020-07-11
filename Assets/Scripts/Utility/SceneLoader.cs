using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Space]
    [Header("INFO")]
    [Space]

    [Space]
    [TextArea(4, 5)]
    public string InfoText = "";
    [Space]

    [Tooltip("This is the build index of the level/levels you want to load")]
    [SerializeField] private List<int> m_ScenesToLoad = new List<int>();

    public static SceneLoader Instance;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        GameManager.Instance.DontDestroyOnLoadList.Add(gameObject);
        if (m_ScenesToLoad.Count != 0)
        {
            foreach (int sceneIndex in m_ScenesToLoad)
            {
                Load(sceneIndex);
            }
        }
    }

    public void Load(int sceneIndex)
    {
        if (!SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }
    }

    public void Unload(int sceneIndex)
    {
        if (SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }
    }
}
