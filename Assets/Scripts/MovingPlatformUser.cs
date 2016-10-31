using UnityEngine;
using System.Collections;

public class MovingPlatformUser : MonoBehaviour {

    GameObject getPlatformFromParent()
    {
        if (transform.parent == null)
        {
            return null;
        }
        foreach (Transform child in transform.parent)
        {
            if (child.gameObject.tag == "Platform")
            {
                return child.gameObject;
            }
        }
        return null;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = collision.transform.parent;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (getPlatformFromParent() == collision.gameObject)
        {
            transform.parent = null;
        }
    }
}
