using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pickup : MonoBehaviour {

    /**
     * List to hold activators to call when this switch is "activated".
     * */
    public List<BaseActivator> activators;
    public GameObject teleportTo;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Avatar")
        {

            foreach (BaseActivator activator in activators)
            {
                 activator.Activate(gameObject);
            }
            Destroy(gameObject);

            if (teleportTo != null)
            {
                Vector3 newPosition = teleportTo.transform.position;
                newPosition.y += 1;
                other.gameObject.transform.position = newPosition;
            }
        }
    }
}
