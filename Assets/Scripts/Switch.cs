using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
    private GameObject attachedChild;

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
            return true;
        }
        return false;
    }

    public void Detach() {
        attachedChild = null;
    } 
}
