using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Switch : MonoBehaviour {
    public bool ignoreMaterial;
    /**
     * List to hold activators to call when this switch is "activated".
     * */
    public List<BaseActivator> activators;
    // to check material matches if activated
    private Renderer materialTile;
    private GameObject attachedChild;

    void Start()
    {
        materialTile = transform.GetChild(0).GetComponent<Renderer>();
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
            child.transform.localPosition = new Vector3(0, 5f, 0);
            child.transform.localRotation = gameObject.transform.rotation;

            attachedChild = child;

            // call activators
            // check material name? if it has one and there is no match then dont activate!
            if (ignoreMaterial || (!ignoreMaterial && materialTile.material.name.Equals(child.GetComponent<Renderer>().material.name)))
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
