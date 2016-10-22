using UnityEngine;
using System.Collections;
using System;

public class Grab : MonoBehaviour, ITargetAnalyzer {
    public Texture2D grabTexture;
    public Texture2D actionTexture;
    private GameObject target;
    private Rigidbody objectRigidBody;
    private bool grabbing;
    private bool inSight;
	
	void Update () {
        DetectTarget();

        if (Input.GetMouseButtonDown(0) && target)
        {
            // check if it can grab something
            if (!grabbing)
            {
                // first check if it's attached to a switch and detach it
                Switch switchTile = target.GetComponentInParent<Switch>();
                if (switchTile)
                {
                    switchTile.Detach();
                }
                // attach to avatar
                target.transform.parent = gameObject.transform;
                // make it kinematic
                objectRigidBody = target.GetComponent<Rigidbody>();
                objectRigidBody.isKinematic = true;
                objectRigidBody.useGravity = false;
                // change position of the grabbed object
                target.transform.position = gameObject.transform.position;
                target.transform.localPosition = new Vector3(1.5f, 0.5f, 2);
                grabbing = true;
                inSight = false;
            }
            else {
                // extra reycast to check if it's looking into a switch to auto attach
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                objectRigidBody.useGravity = true;
                // Cast a ray
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag.Equals("Switch"))
                {
                    Switch script = hit.collider.gameObject.GetComponent<Switch>();
                    // if it's null it might be in the parent! (It's pointing to the center of the switch which is another object)
                    script = hit.collider.gameObject.GetComponentInParent<Switch>();
                    // attach it? if returns false then the switch is already taken so it's ignored
                    if (!script.Attach(target)) return;
                }
                else
                {
                    // release parent, activate gravity and remove kinematic value
                    target.transform.parent = null;
                    objectRigidBody.isKinematic = false;
                }
                // clear references and allow grabbing again
                target = null;
                objectRigidBody = null;
                grabbing = false;
            }
        }
    }

    private void DetectTarget()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && 
            (hit.collider.gameObject.tag.Equals("Grabbable") || (hit.collider.gameObject.tag.Equals("Switch") && grabbing ) ))
        {
            // only pre assign if it's a grabbable object, if it's a switch let the code in the update handle it
            // this is only to activate the crosshair
            // only set if it's nothing on grab at the moment!
            if (hit.collider.gameObject.tag.Equals("Grabbable") && !grabbing)
            {
                target = hit.collider.gameObject;
            }
            inSight = true;
        }else
        {
            // if the player looked at the object but did not grab it then clear this reference
            if (!grabbing)
            {
                target = null;
            }
            inSight = false;
        }
    }

    public bool InSight()
    {
        return inSight;
    }

    public Texture2D GetInSightTexture()
    {
        return grabbing ? actionTexture : grabTexture;
    }
}