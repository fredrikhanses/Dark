using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraShiftInteractable))]
public class CameraShiftInteractableEditor : Editor
{
    private void OnSceneGUI()
    {
        CameraShiftInteractable shift = (CameraShiftInteractable)target;

        if (Tools.current == Tool.Rotate)
        {
            shift.m_Direction = Handles.RotationHandle(shift.m_Direction, shift.m_TargetPosition);
            Handles.color = Handles.zAxisColor;
            Handles.ArrowHandleCap(0, shift.m_TargetPosition, shift.m_Direction, 1, EventType.Repaint);
            Handles.color = Handles.yAxisColor;
            Handles.ArrowHandleCap(0, shift.m_TargetPosition, shift.m_Direction * Quaternion.LookRotation(Vector3.up), 1, EventType.Repaint);

        }
        else
        {
            shift.m_TargetPosition = Handles.PositionHandle(shift.m_TargetPosition, Quaternion.identity);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CameraShiftInteractable shift = (CameraShiftInteractable)target;

        if (shift.m_EndCondition == CameraShiftInteractable.EndCondition.Timer)
        {
            shift.m_WaitTime = EditorGUILayout.FloatField("Timer Duration", shift.m_WaitTime);
        }

        if (GUILayout.Button("Reset Target"))
        {
            shift.m_TargetPosition = shift.transform.position;
        }
    }
}
