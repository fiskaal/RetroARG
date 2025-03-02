using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    
    
    [SerializeField]private Transform[] points;

    public Color gizmoColor = Color.red;

    private void OnDrawGizmos()
    {
        if (points == null || points.Length < 2)
        {
            // Not enough points to draw lines
            return;
        }

        // Set Gizmos color
        Gizmos.color = gizmoColor;

        // Draw lines between consecutive points
        for (int i = 0; i < points.Length - 1; i++)
        {
            if (points[i] != null && points[i + 1] != null)
            {
                // Draw line
                Gizmos.DrawLine(points[i].position, points[i + 1].position);

                // Draw arrowhead in the middle of the line
                Vector3 midPoint = (points[i].position + points[i + 1].position) / 2;
                Vector3 direction = (points[i + 1].position - points[i].position).normalized;
                Vector3 arrowHead1 = Quaternion.Euler(0, 30, 0) * -direction * 0.2f;
                Vector3 arrowHead2 = Quaternion.Euler(0, -30, 0) * -direction * 0.2f;

                Gizmos.DrawLine(midPoint, midPoint + arrowHead1);
                Gizmos.DrawLine(midPoint, midPoint + arrowHead2);
            }
        }
    }

}
