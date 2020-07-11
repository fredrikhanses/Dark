using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Mother))]
public class MotherEditor : Editor
{
    private void OnSceneGUI()
    {
        Mother mom = (Mother)target;

        if (Tools.current == Tool.Rotate)
        {
            mom.m_UnCorruptCameraShiftRotation = Handles.RotationHandle(mom.m_UnCorruptCameraShiftRotation, mom.m_UnCorruptCameraShiftPosition);
            Handles.color = Handles.zAxisColor;
            Handles.ArrowHandleCap(0, mom.m_UnCorruptCameraShiftPosition, mom.m_UnCorruptCameraShiftRotation, 1, EventType.Repaint);
            Handles.color = Handles.yAxisColor;
            Handles.ArrowHandleCap(0, mom.m_UnCorruptCameraShiftPosition, mom.m_UnCorruptCameraShiftRotation * Quaternion.LookRotation(Vector3.up), 1, EventType.Repaint);

            mom.m_CorruptCameraShiftRotation = Handles.RotationHandle(mom.m_CorruptCameraShiftRotation, mom.m_CorruptCameraShiftPosition);
            Handles.color = Handles.zAxisColor;
            Handles.ArrowHandleCap(0, mom.m_CorruptCameraShiftPosition, mom.m_CorruptCameraShiftRotation, 1, EventType.Repaint);
            Handles.color = Handles.yAxisColor;
            Handles.ArrowHandleCap(0, mom.m_CorruptCameraShiftPosition, mom.m_CorruptCameraShiftRotation * Quaternion.LookRotation(Vector3.up), 1, EventType.Repaint);

            mom.m_FullCorruptCameraShiftRotation = Handles.RotationHandle(mom.m_FullCorruptCameraShiftRotation, mom.m_FullCorruptCameraShiftPosition);
            Handles.color = Handles.zAxisColor;
            Handles.ArrowHandleCap(0, mom.m_FullCorruptCameraShiftPosition, mom.m_FullCorruptCameraShiftRotation, 1, EventType.Repaint);
            Handles.color = Handles.yAxisColor;
            Handles.ArrowHandleCap(0, mom.m_FullCorruptCameraShiftPosition, mom.m_FullCorruptCameraShiftRotation * Quaternion.LookRotation(Vector3.up), 1, EventType.Repaint);
        }
        else
        {
            mom.m_UnCorruptCameraShiftPosition = Handles.PositionHandle(mom.m_UnCorruptCameraShiftPosition, Quaternion.identity);
            mom.m_CorruptCameraShiftPosition = Handles.PositionHandle(mom.m_CorruptCameraShiftPosition, Quaternion.identity);
            mom.m_FullCorruptCameraShiftPosition = Handles.PositionHandle(mom.m_FullCorruptCameraShiftPosition, Quaternion.identity);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Mother mom = (Mother)target;

        if (GUILayout.Button("Set Un Corrupted Camera"))
        {
            Camera sceneCam = SceneView.GetAllSceneCameras()[0];
            mom.m_UnCorruptCameraShiftPosition = sceneCam.transform.position;
            mom.m_UnCorruptCameraShiftRotation = sceneCam.transform.rotation;
        }

        if (GUILayout.Button("Set Corrupted Camera"))
        {
            Camera sceneCam = SceneView.GetAllSceneCameras()[0];
            mom.m_CorruptCameraShiftPosition = sceneCam.transform.position;
            mom.m_CorruptCameraShiftRotation = sceneCam.transform.rotation;
        }

        if (GUILayout.Button("Set Full Corrupted Camera"))
        {
            Camera sceneCam = SceneView.GetAllSceneCameras()[0];
            mom.m_FullCorruptCameraShiftPosition = sceneCam.transform.position;
            mom.m_FullCorruptCameraShiftRotation = sceneCam.transform.rotation;
        }
    }
}