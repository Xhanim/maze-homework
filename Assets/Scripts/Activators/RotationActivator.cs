using UnityEngine;
using System.Collections;
using System;

public class RotationActivator : BaseActivator {

    public float rotateBy = 180;
    public float rotationTime = 1;
    private bool rotating;
    private Vector3 originalAngle;
    private float currentTime = 0;

    public override void Activate(GameObject trigger)
    {
        if (rotating)
        {
            return;
        }
        currentTime = 0;
        originalAngle = gameObject.transform.eulerAngles;
        rotating = true;
    }

    public override void Desactivate()
    {
        rotating = false;
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update()
    {
        if (!rotating)
        {
            return;
        }
        float timeStep = currentTime + Time.deltaTime;
        float step = timeStep / rotationTime;
        float rotationAngle = Mathf.Lerp(0, rotateBy, step);
        Vector3 newRotation = new Vector3(0, rotationAngle, 0) + originalAngle;
        transform.Rotate(newRotation - transform.rotation.eulerAngles);
        currentTime = timeStep;
        if (step >= 1)
        {
            rotating = false;
        }
    }
}
