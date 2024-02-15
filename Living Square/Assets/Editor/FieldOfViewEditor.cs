using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
   private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView) target;
        
        // draws 360 circle using fov.radius
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        // draws 360 circle using fov.spottedDistance
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector2.up, Vector3.forward, 360, fov.spottedDistance);

        // gets the 2 angles from target using half fov view angle
        Vector3 viewAngle1 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.viewAngle / 2);
        Vector3 viewAngle2 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.viewAngle / 2);

        // draws 2 yellow lines to represent FOV using the two angles retrieved above
        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle1 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.radius);

        // draws a line from target to player if playerSeen is true
        if (fov.playerSeen)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.player.transform.position);
        }
    }

    // method to calculate angle from target
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
