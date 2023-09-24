#pragma strict

function Start () {

}

function Update () {

if(Input.GetKey("a"))
	transform.position.x-=Time.deltaTime;
if(Input.GetKey("d"))
	transform.position.x+=Time.deltaTime;
	

if(Input.GetKeyDown("w"))
//	transform.GetComponent.<Rigidbody2D>().AddForce(Vector2(0,30),ForceMode.Force);
GetComponent.<Rigidbody2D>().AddForce (new Vector2 (0, 300));
//	rigidbody2D.AddForce(Vector3.up * 10 * Time.deltaTime);
	
}