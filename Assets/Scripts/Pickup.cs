using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pickup : MonoBehaviour {

    /**
     * List to hold activators to call when this switch is "activated".
     * */
    public List<BaseActivator> activators;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Avatar")
        {

            foreach (BaseActivator activator in activators)
            {
                 activator.Activate(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
