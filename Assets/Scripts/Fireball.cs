using UnityEngine;
using System.Collections;
using System;

public class Fireball : MonoBehaviour, TargetAnalyzer {
    public GameObject prefab;
    public GameObject spawn;

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
            GameObject ballInstance = Instantiate(prefab, spawn.transform.position, spawn.transform.rotation) as GameObject;
            ballInstance.GetComponent<Rigidbody>().AddForce(spawn.transform.forward * 2500);
        }
    }
}
