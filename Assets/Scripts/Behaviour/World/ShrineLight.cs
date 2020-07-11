using UnityEngine;

public class ShrineLight : MonoBehaviour
{
    [SerializeField] private Light m_Light;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameManager.Instance.DontDestroyOnLoadList.Add(gameObject);
    }

    public void SetEnable(bool enable)
    {
        m_Light.enabled = enable;
    }
}
