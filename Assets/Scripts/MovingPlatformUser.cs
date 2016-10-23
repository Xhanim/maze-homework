using UnityEngine;
using System.Collections;

public class MovingPlatformUser : MonoBehaviour {

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = collision.transform.parent;
        } else
        {
            transform.parent = null;
        }
    }
}
