using UnityEngine;
using System.Collections;
using System;

public class Fireball : MonoBehaviour, TargetAnalyzer {
    public GameObject prefab;
    public GameObject spawn;
    public float distance = 10;
    public float force = 2500;

    public bool InSight()
    {
        return false;
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            // Make spawn look at camera position
            Vector3 cameraPosition = Camera.main.transform.position;
            cameraPosition += Camera.main.transform.forward * distance;
            spawn.transform.LookAt(cameraPosition);
            GameObject ballInstance = Instantiate(prefab, spawn.transform.position, spawn.transform.rotation) as GameObject;
            ballInstance.GetComponent<Rigidbody>().AddForce(spawn.transform.forward * force);
        }
    }
}
