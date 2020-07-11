using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EventGate : MonoBehaviour
{
    public GateBoolean[] m_Triggers;
    [SerializeField] private GameObject[] m_EventRecievers;
    [SerializeField] private bool m_SingleTrigger;
    [SerializeField] private float m_Delay;
    private bool m_IsTriggered = false;

    private void Awake()
    {
        foreach (var t in m_Triggers)
        {
            t.SetGate(this);
        }
    }

    public void CheckObjectTriggers()
    {
        if (m_IsTriggered)
            return;

        bool t = true;

        foreach (var b in m_Triggers)
        {
            if (b.Get())
            {
                if (b.m_State == GateBoolean.LogicState.Or && t)
                {
                    Trigger();
                    break;
                }
            }
            else
            {
                if (b.m_State == GateBoolean.LogicState.And)
                    t = false;
            }

            if (b.m_State == GateBoolean.LogicState.Or)
                t = true;
        }

        if (t)
            Trigger();

        if (!m_SingleTrigger)
            m_IsTriggered = false;
    }

    private void Trigger()
    {
        foreach (GameObject go in m_EventRecievers)
        {
            var mb = go.GetComponents<MonoBehaviour>().OfType<IEvent>();

            if (mb != null)
            {
                foreach (var i in mb)
                {
                    i.Event();
                }
            }
            else
            {
                //If you reach this your script you attached does not have an IEvent interface implemented.
                throw new System.DataMisalignedException("Event Reciever Object \"" + go.name + "\" has no IEvent scripts attatched. Please attach a compatible script");
            }
        }

        m_IsTriggered = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
        Gizmos.color = Color.magenta;

        foreach (GateBoolean mb in m_Triggers)
        {
            if (mb != null)
            {
                Transform t = mb.transform;
                Gizmos.DrawLine(transform.position, t.position);
                Gizmos.DrawWireCube(t.position, t.localScale);

                if (mb.m_State == GateBoolean.LogicState.Or)
                {
                    if (Gizmos.color == Color.magenta)
                        Gizmos.color = Color.blue;
                    else
                        Gizmos.color = Color.magenta;
                }
                else
                {
                    int i = Array.IndexOf(m_Triggers, mb);
                    if (i <= m_Triggers.Length - 2 && m_Triggers[i + 1] != null)
                    {
                        Gizmos.DrawLine(m_Triggers[i].transform.position, m_Triggers[i + 1].transform.position);
                    }
                }
            }

        }

        foreach (GameObject go in m_EventRecievers)
        {
            Transform t = go.transform;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, t.position);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(t.position, t.localScale);
        }
    }
}
