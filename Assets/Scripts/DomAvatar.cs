using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {

	private float movementSpeed = 20;
	private float gravity = -5;
	private CharacterController myController = null;
	private Vector3 moveDirection = Vector3.zero;

	private float rotationSpeed = 90;
	private Vector3 rotationDirection = Vector3.zero;

	void Awake ()
	{
		myController = GetComponent<CharacterController>();
	}

	void Update () 
	{
		moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Vertical") * movementSpeed, gravity, 0));
		myController.Move(moveDirection * Time.deltaTime);

		rotationDirection = new Vector3(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
		transform.Rotate(rotationDirection * Time.deltaTime, Space.Self);
	}
}



