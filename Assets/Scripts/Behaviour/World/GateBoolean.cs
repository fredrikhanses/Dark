using UnityEngine;

public class GateBoolean : MonoBehaviour
{
    public enum LogicState
    {
        And,
        Or
    }

    bool m_Active;
    [HideInInspector] public LogicState m_State;
    [System.NonSerialized] public EventGate m_EventHandler;

    public virtual void Toggle()
    {
        m_Active = !m_Active;
        m_EventHandler.CheckObjectTriggers();
    }

    public virtual void Set(bool b)
    {
        if (m_Active != b)
        {
            m_Active = b;
            m_EventHandler.CheckObjectTriggers();
        }

    }

    public virtual bool Get()
    {
        return m_Active;
    }

    public virtual void SetGate(EventGate gate)
    {
        m_EventHandler = gate;
    }
}
