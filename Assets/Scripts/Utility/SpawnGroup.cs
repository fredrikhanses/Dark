using UnityEngine;

public class SpawnGroup : MonoBehaviour, IEvent
{
    [TextArea(4, 5)]
    [SerializeField] private string m_Info;

    private const string k_SpawnChild = "SpawnChild";

    public void Event()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        foreach (Transform child in transform)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, child.transform.position);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(child.transform.position, child.transform.localScale);
            Gizmos.DrawIcon(child.transform.position, k_SpawnChild, true, Color.red);
        }
        Gizmos.color = Color.white; 
    }
#endif
}
