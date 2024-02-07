using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDrawer : MonoBehaviour
{
    public static Action drawAction;

    private void OnApplicationQuit()
    {
        drawAction = null;
    }

    private void OnDrawGizmos()
    {
        drawAction?.Invoke();
    }

    public static void DrawArc(Transform transform, float radius, float angle, Color color)
    {
        drawAction = () =>
        {
            Gizmos.color = color;
            Vector3 left = Quaternion.Euler(0.0f, -angle / 2.0f, 0.0f) * transform.forward;
            Vector3 right = Quaternion.Euler(0.0f, angle / 2.0f, 0.0f) * transform.forward;
            int segments = 10;
            Vector3 prev = transform.position + radius * left;
            for (int i = 0; i < segments; i++)
            {
                float ratio = (float)(i + 1) / segments;
                float theta = Mathf.Lerp(-angle / 2.0f, angle / 2.0f, ratio);
                Vector3 next = transform.position +
                               Quaternion.Euler(0f, theta, 0f) * transform.forward * radius;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
            Gizmos.DrawLine(transform.position, transform.position + left * radius);
            Gizmos.DrawLine(transform.position, transform.position + right * radius);
        };
    }
}
