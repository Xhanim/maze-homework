using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Switch : MonoBehaviour {
    public Cube.CubeType type;
    /**
     * List to hold activators to call when this switch is "activated".
     * */
    public List<BaseActivator> activators;
    private GameObject attachedChild;

    void Start()
    {
    }

    /**
     * Attaches a new game object as a child for this switch and activates something?
     * */
	public bool Attach(GameObject child)
    {
        if (!attachedChild)
        {
            child.transform.parent = gameObject.transform;
            child.transform.position = gameObject.transform.position;
            child.transform.localPosition = new Vector3(0, 0, 0);
            child.transform.localRotation = new Quaternion(0, 0, 0, 0);// new Vector3(0, 0, 0); gameObject.transform.rotation;

            attachedChild = child;

            Debug.Log("cube material: " + child.GetComponent<Renderer>().material.name);
            // call activators
            // check material name? if it has one and there is no match then dont activate!
            if (type == child.GetComponent<Cube>().type)
            {
                foreach (BaseActivator activator in activators)
                {
                    activator.Activate(attachedChild);
                }
            }

            return true;
        }
        return false;
    }

    public void Detach() {
        attachedChild = null;
        foreach (BaseActivator activator in activators)
        {
            activator.Desactivate();
        }
    } 
}
