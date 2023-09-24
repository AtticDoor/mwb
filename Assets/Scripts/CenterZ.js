#pragma strict



function Update () {
transform.position.z=0;
	//Update2();
}


	function Update2 () {
		var controller : CharacterController = GetComponent(CharacterController);
		if (controller.isGrounded)
			print("We are grounded");
		else print("Not Grounded");
	}