using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMovement : MonoBehaviour {
	Vector3 moveJump = Vector3.zero;
	float horMove, vertMove;
	void Start(){
		SheetAssigner SA = FindObjectOfType<SheetAssigner>();
		Vector3 tempJump = SA.roomDimensions + SA.gutterSize;
		moveJump = new Vector3(tempJump.x, 0, tempJump.z); //distance b/w rooms: to be used for movement
	}
	void Update()
	{
		if (Input.GetKeyDown("w") || Input.GetKeyDown("s") || 
			Input.GetKeyDown("a") || Input.GetKeyDown("d")) //if any 'wasd' key is pressed
		{
			horMove = System.Math.Sign(Input.GetAxisRaw("Horizontal"));//capture input
			vertMove = System.Math.Sign(Input.GetAxisRaw("Vertical"));
			Vector3 tempPos = transform.position;
			tempPos += Vector3.right * horMove * moveJump.x; //jump between rooms based on input
			tempPos += Vector3.forward * vertMove * moveJump.z;
			transform.position = tempPos;
		}
	}
}
