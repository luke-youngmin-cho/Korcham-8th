using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TargetDetector))]
public class TargetDetectorDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.white;
        float angle = 90.0f;
        TargetDetector targetDetector = (TargetDetector)target;
        Handles.DrawWireArc(targetDetector.transform.position,
                            targetDetector.transform.up,
                            Quaternion.Euler(0f, -angle / 2.0f, 0f) * targetDetector.transform.forward,
                            angle,
                            7);
    }
}
