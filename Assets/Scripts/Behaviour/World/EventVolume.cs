//©©©©©©©Samuel Gustafsson©©©©©©©2020©©©©©©©©

using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Collider))]
public class EventVolume : MonoBehaviour {
    [SerializeField] private GameObject[] m_EventRecievers;
    [SerializeField] private GameObject[] m_ExitEventRecievers;
    [SerializeField] private bool m_SingleTrigger;
    [SerializeField] private bool m_ExitTrigger;
    [SerializeField] private string m_TriggerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_TriggerTag))
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

            if (m_SingleTrigger)
                gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(m_TriggerTag) && m_ExitTrigger)
        {
            foreach (GameObject go in m_ExitEventRecievers)
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
        }
    }

    private void Start()
    {
        //If you reach this the trigger volume you are using is not set to a trigger.
        if (!GetComponent<Collider>().isTrigger)
        {
            throw new System.DataMisalignedException("Trigger Volume \"" + gameObject.name + "\" is not set to be a trigger collider.");
        }
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

        if (m_ExitEventRecievers.Length > 0)
        {
            foreach (GameObject go in m_ExitEventRecievers)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, go.transform.position);
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(go.transform.position, go.transform.localScale);
            }
        }

        Gizmos.color = Color.white;
    }
}
