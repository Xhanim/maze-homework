using UnityEngine;
using System.Collections;
using System;

public class FireballShooter : MonoBehaviour, ITargetAnalyzer {
    public GameObject prefab;
    public GameObject spawn;
    public float aimCenterDistance = 10;
    public float impulse = 50;
    public Texture2D crosshair;
    private Camera camera;

    void Start()
    {
        camera = GetComponentInChildren<Camera>();
    }

    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            // Make spawn look at camera position
            Transform cameraTransform = camera.transform;
            Vector3 cameraPosition = cameraTransform.position;
            cameraPosition += cameraTransform.forward * aimCenterDistance;
            spawn.transform.LookAt(cameraPosition);
            GameObject ballInstance = Instantiate(prefab, spawn.transform.position, spawn.transform.rotation) as GameObject;
            ballInstance.GetComponent<Rigidbody>().AddForce(spawn.transform.forward * impulse, ForceMode.Impulse);
        }
    }

    public bool InSight()
    {
        return true;
    }

    public Texture2D GetInSightTexture()
    {
        return crosshair;
    }
}
