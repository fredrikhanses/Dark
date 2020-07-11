using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventGate))]
public class EventGateEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var eventGate = (EventGate)target;
        var triggers = eventGate.m_Triggers;

        GUILayout.Space(10);
        GUILayout.Label("Logic:", EditorStyles.boldLabel);
        GUILayout.Space(5);

        if (triggers.Length > 0)
        {
            for (int i = 0; i < triggers.Length; i++)
            {
                if (triggers[i] != null)
                {
                    GUILayout.Label(triggers[i].name);

                    if (i + 1 < triggers.Length)
                    {
                        if (GUILayout.Button(triggers[i].m_State.ToString(), GUILayout.MaxWidth(50)))
                        {
                            if (triggers[i].m_State == GateBoolean.LogicState.And)
                                triggers[i].m_State = GateBoolean.LogicState.Or;
                            else
                                triggers[i].m_State = GateBoolean.LogicState.And;
                        }
                    }
                }
            }
        }
        GUILayout.Space(10);
        GUILayout.Label("INFO:", EditorStyles.boldLabel);
        GUILayout.Space(5);
        GUILayout.Label("This script will send an event to all the targeted Game Objects bellow.\n" +
            " The target objects MUST IEvent interface implamented in order to work.\n" +
            " Please use the scripts in Scripts/Behaviour/World/EventObjects");
    }
}
