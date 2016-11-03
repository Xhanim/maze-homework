using UnityEngine;
using System.Collections;

public class CubeRespawn : MonoBehaviour {
    private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
	}

    public void OnTriggerEnter(Collider other)
    {
        // if players tries to leave the room with a cube
        if (other.gameObject.CompareTag("CubeDestroyer"))
        {
            GameObject.FindGameObjectWithTag("Avatar").GetComponent<Grab>().release();
            transform.position = initialPosition;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        // if released out of boundaries or something
        if (collision.gameObject.CompareTag("Void"))
        {
            transform.position = initialPosition;
        }
    }
}
