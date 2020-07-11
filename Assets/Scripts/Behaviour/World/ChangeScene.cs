using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, IEvent
{
    [SerializeField, Range(0f, 10f)] private float m_LoadSceneDelay = 1f;
    [SerializeField, Range(0, 10)] private int m_SceneIndex = 1;
    [SerializeField] private SoundManager m_SoundManager;

    private const string k_LoadScene = "LoadScene";

    private void Start()
    {
        if (m_SoundManager == null)
        {
            m_SoundManager = GetComponent<SoundManager>();
        }
    }

    public void Event()
    {
        m_SoundManager.PlaySound(k_LoadScene);
        Invoke(k_LoadScene, m_LoadSceneDelay);
    }

    private void LoadScene()
    {
        SceneManager.LoadSceneAsync(m_SceneIndex);
    }
}
