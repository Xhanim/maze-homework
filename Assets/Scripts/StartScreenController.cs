using UnityEngine;
using System.Collections;

public class StartScreenController : MonoBehaviour {

    public Transition startTransition;

	void Start () {
	
	}
	
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startTransition.StartTransition();
        }
	}
}
