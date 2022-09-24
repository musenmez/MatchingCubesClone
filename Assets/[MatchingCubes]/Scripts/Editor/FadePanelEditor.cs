#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FadePanelBase), true)]
public class FadePanelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FadePanelBase fadePanel = (FadePanelBase)target;
        if (GUILayout.Button("Toggle Panel"))
        {
            fadePanel.TogglePanel();
        }
    }
}
#endif