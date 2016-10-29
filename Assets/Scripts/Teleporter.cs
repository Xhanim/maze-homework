using UnityEngine;
using System.Collections;
using System;

public class Teleporter : MonoBehaviour, ITargetAnalyzer
{
    public Texture2D crosshair;
    public float maxDistance = 13;
    private TeleportWaypoint teleportWaypoint;
    private Camera camera;

    void Start()
    {
        camera = GetComponentInChildren<Camera>();
    }

    void Update () {
        UpdateDetectedWaypoint();
        if (Input.GetMouseButtonDown(0) && teleportWaypoint != null)
        {
            teleportWaypoint.TeleportObject(gameObject);
        }
    }

    void UpdateDetectedWaypoint()
    {
        Transform cameraTransform = camera.transform;
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && maxDistance >= hit.distance)
        {
            GameObject hitObject = hit.collider.gameObject;
            teleportWaypoint = hitObject.GetComponent<TeleportWaypoint>();
        } else
        {
            teleportWaypoint = null;
        }
    }

    public bool InSight()
    {
        return teleportWaypoint != null;
    }

    public Texture2D GetInSightTexture()
    {
        return crosshair;
    }
}