//©©©©©©©Samuel Gustafsson©©©©©©©2020©©©©©©©©

using UnityEngine;
using UnityEngine.Assertions;
public class EventOnStart : MonoBehaviour
{
    [SerializeField] private GameObject[] m_EventRecievers;

    private void Start()
    {
        Trigger();
    }

    private void Trigger()
    {
        foreach (GameObject go in m_EventRecievers)
        {
            MonoBehaviour mb = go.GetComponent<MonoBehaviour>();

            if (mb is IEvent)
            {
                IEvent eventCall = (IEvent)mb;
                eventCall.Event();
            }
            else
            {
                //If you reach this your script you attached does not have an IEvent interface implemented.
                throw new System.DataMisalignedException("Event Reciever Object \"" + go.name + "\" has no IEvent scripts attatched. Please attach a compatible script");
            }
        }

        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);

        if (m_EventRecievers.Length > 0)
        {
            foreach (GameObject go in m_EventRecievers)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, go.transform.position);
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(go.transform.position, go.transform.localScale);
            }
        }

        Gizmos.color = Color.white;
    }
}
