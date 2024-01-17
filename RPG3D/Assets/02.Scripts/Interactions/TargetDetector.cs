using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private CapsuleCollider _capsuleCastShape;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RayCast();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            OverlapSphere();
        }
    }

    public Collider RayCast()
    {
        if (Physics.Raycast(transform.position,
                            transform.forward,
                            out RaycastHit hit,
                            float.PositiveInfinity,
                            _targetMask))
        {
            Debug.DrawLine(transform.position,
                           transform.position + transform.forward * 1000,
                           Color.blue,
                           1.0f);
            return hit.collider;
        }

        Debug.DrawLine(transform.position,
                       transform.position + transform.forward * 1000,
                       Color.red,
                       1.0f);
        return null;
    }

    public RaycastHit[] CapsuleCastAll()
    {
        RaycastHit[] hits =
        Physics.CapsuleCastAll(_capsuleCastShape.transform.position + _capsuleCastShape.center + Vector3.down * _capsuleCastShape.height / 2.0f + Vector3.up * _capsuleCastShape.radius,
                               _capsuleCastShape.transform.position + _capsuleCastShape.center + Vector3.up * _capsuleCastShape.height / 2.0f + Vector3.down * _capsuleCastShape.radius,
                               _capsuleCastShape.radius,
                               transform.forward,
                               4.0f,
                               _targetMask);

        return hits;
    }

    public Collider[] OverlapSphere()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 5.0f, _targetMask);

        for (int i = 0; i < cols.Length; i++)
        {
            Debug.Log($"Overlapped {cols[i].name}");
        }

        return cols;
    }


    private void OnDrawGizmos()
    {
        if (_capsuleCastShape == null)
            return;

        Gizmos.color = Color.cyan;

        Vector3 p1 = _capsuleCastShape.transform.position + _capsuleCastShape.center + Vector3.down * _capsuleCastShape.height / 2.0f + Vector3.up * _capsuleCastShape.radius;
        Vector3 p2 = _capsuleCastShape.transform.position + _capsuleCastShape.center + Vector3.up * _capsuleCastShape.height / 2.0f + Vector3.down * _capsuleCastShape.radius;
        Vector3 p3 = p1 + transform.forward * 4.0f;
        Vector3 p4 = p2 + transform.forward * 4.0f;
        DrawCapsule(p1, p2, _capsuleCastShape.radius, Color.cyan);
        DrawCapsule(p3, p4, _capsuleCastShape.radius, Color.cyan);
        Gizmos.DrawLine(p1 - transform.up * _capsuleCastShape.radius, p3 - transform.up * _capsuleCastShape.radius);
        Gizmos.DrawLine(p1 + transform.right * _capsuleCastShape.radius, p3 + transform.right * _capsuleCastShape.radius);
        Gizmos.DrawLine(p1 - transform.right * _capsuleCastShape.radius, p3 - transform.right * _capsuleCastShape.radius);
        Gizmos.DrawLine(p2 + transform.up * _capsuleCastShape.radius, p4 + transform.up * _capsuleCastShape.radius);
        Gizmos.DrawLine(p2 + transform.right * _capsuleCastShape.radius, p4 + transform.right * _capsuleCastShape.radius);
        Gizmos.DrawLine(p2 - transform.right * _capsuleCastShape.radius, p4 - transform.right * _capsuleCastShape.radius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5.0f);

        DrawArc(3, 45, Color.green);
    }

    private void DrawCapsule(Vector3 p1, Vector3 p2, float radius, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(p1, radius);
        Gizmos.DrawWireSphere(p2, radius);
        Gizmos.DrawLine(p1 + transform.forward * radius, p2 + transform.forward * radius);
        Gizmos.DrawLine(p1 - transform.forward * radius, p2 - transform.forward * radius);
        Gizmos.DrawLine(p1 + transform.right * radius, p2 + transform.right * radius);
        Gizmos.DrawLine(p1 - transform.right * radius, p2 - transform.right * radius);
    }

    private void DrawArc(float radius, float angle, Color color)
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
    }

}
