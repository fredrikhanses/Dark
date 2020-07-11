using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventVolume))]
public class EventVolumeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("INFO:", EditorStyles.boldLabel);
        GUILayout.Space(5);
        GUILayout.Label("This script will send an event to all the targeted Game Objects bellow.\n" +
            " The target objects MUST IEvent interface implamented in order to work.\n" +
            " Please use the scripts in Scripts/Behaviour/World/EventObjects");
    }
}
