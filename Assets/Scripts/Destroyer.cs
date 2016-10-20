using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
    public float destroyAfter = 1.5f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyAfter);
    }
}
