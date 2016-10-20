using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    private TeleportWaypoint teleportWaypoint;

    void Update () {
        UpdateDetectedWaypoint();
        if (Input.GetMouseButtonDown(0) && teleportWaypoint != null)
        {
            teleportWaypoint.TeleportObject(gameObject);
        }
    }

    void UpdateDetectedWaypoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            teleportWaypoint = hitObject.GetComponentInChildren<TeleportWaypoint>();
        } else
        {
            teleportWaypoint = null;
        }
    }

    public bool IsWaypointInSight()
    {
        return teleportWaypoint != null;
    }
}