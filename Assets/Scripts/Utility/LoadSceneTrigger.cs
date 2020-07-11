using System.Collections;
using UnityEngine;

public class LoadSceneTrigger : MonoBehaviour, IEvent
{
    [Tooltip("The build index of scene you want to load goes here")]
    [SerializeField] private int m_LoadIndex = -1;
    [Tooltip("The build index of scene you want to unload goes here")]
    [SerializeField] private int m_UnloadIndex = -1;
    [Tooltip("Number of the shrine to enter to")]
    [SerializeField, Range(0, 3)] private int m_ShrineNumber;
    [SerializeField] private MeshRenderer m_MeshRenderer;
    [SerializeField] private float m_LoadDelay = 0f;
    [SerializeField] private float m_UnloadDelay = 0f;
    [SerializeField] private SoundManager m_SoundManager;
    [SerializeField] private Material m_WallMaterial;
    [SerializeField] private BoxCollider m_BoxCollider;

    public int ShrineNumber { get => m_ShrineNumber; }
    public int LoadIndex { get => m_LoadIndex; set => m_LoadIndex = value; }

    private const string k_PlayerTag = "Player";
    private const string k_UnloadScene = "UnloadScene";
    private const string k_LoadScene = "LoadScene";

    private void Awake()
    {
        if (m_MeshRenderer == null)
        {
            m_MeshRenderer = GetComponent<MeshRenderer>();
        }
        if (m_SoundManager == null)
        {
            m_SoundManager = GetComponent<SoundManager>();
        }
        if (m_BoxCollider == null)
        {
            m_BoxCollider = GetComponent<BoxCollider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(k_PlayerTag) && m_LoadIndex >= 0)
        {
            StartCoroutine(k_LoadScene);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(k_PlayerTag) && m_UnloadIndex >= 0)
        {
            StartCoroutine(k_UnloadScene);
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(m_LoadDelay);
        SceneLoader.Instance.Load(m_LoadIndex);
    }

    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(m_UnloadDelay);
        SceneLoader.Instance.Unload(m_UnloadIndex);
    }

    public void Deactivate()
    {
        m_MeshRenderer.enabled = false;
        m_BoxCollider.enabled = false;
    }

    public void Event()
    {
        if (m_LoadIndex >= 0)
        {
            StartCoroutine(k_LoadScene);
        }
        if (m_UnloadIndex >= 0)
        {
            StartCoroutine(k_UnloadScene);
        }
    }
}
