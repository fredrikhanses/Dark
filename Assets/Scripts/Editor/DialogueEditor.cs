using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("INFO:\n" +
            "The dialog segements are split into array elements.\n" +
            "'.' and ',' will cause the text scrolling to pause for a little bit.\n" +
            "You can also use '/#' to create a pause that is not visible in the dialog.\n" +
            "Use / on any pause symbol to cancel the pause.");
    }
}
