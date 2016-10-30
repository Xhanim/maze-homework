using UnityEngine;
using System.Collections;

public class TrailPoint : MonoBehaviour {

	// Use this for initialization
	void OnDrawGizmos () {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(gameObject.transform.position, 0.5f);
	}
	
}
