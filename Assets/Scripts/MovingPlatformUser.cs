using UnityEngine;
using System.Collections;

public class MovingPlatformUser : MonoBehaviour {

    private bool inPlatform;
    private bool collided;

    void Update()
    {
        if (collided && !inPlatform)
        {
            transform.parent = null;
        }
        inPlatform = false;
        collided = false;
    }

    void OnCollisionStay(Collision collision)
    {
        collided = true;
        if (collision.gameObject.tag == "Platform")
        {
            inPlatform = true;
            transform.parent = collision.transform.parent;
        }
    }
}
