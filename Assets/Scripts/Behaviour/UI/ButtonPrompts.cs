using UnityEngine;
using UnityEngine.UI;

public class ButtonPrompts : MonoBehaviour
{
    [SerializeField] private GameObject m_ButtonPrompt;
    [SerializeField] private string m_TextToShow;
    private Text m_Prompt;

    private void Awake()
    {
        m_Prompt = m_ButtonPrompt.GetComponent<Text>();
        m_ButtonPrompt.SetActive(false);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.DontDestroyOnLoadList.Add(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_Prompt.text = m_TextToShow;
        m_ButtonPrompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        m_ButtonPrompt.SetActive(false);
    }
}
