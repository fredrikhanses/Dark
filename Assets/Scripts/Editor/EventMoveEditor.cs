//©©©©©©©Samuel Gustafsson©©©©©©©2020©©©©©©©©

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventMove))]
public class EventMoveEditor : Editor
{
    private void OnSceneGUI()
    {
        EventMove eventMove = (EventMove)target;
        eventMove.m_TargetPosition = Handles.PositionHandle(eventMove.m_TargetPosition, Quaternion.identity);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EventMove eventMove = (EventMove)target;

        if (GUILayout.Button("Reset Target"))
        {
            eventMove.m_TargetPosition = eventMove.transform.position;
        }
    }
}
