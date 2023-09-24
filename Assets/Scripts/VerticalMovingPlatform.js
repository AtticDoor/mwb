#pragma strict

function Start () {

}




function OnTriggerEnter(hit : Collider)
{
	if(hit.tag=="Player")
   		hit.transform.parent = transform;
}
 
function OnTriggerExit(hit : Collider)
{
	if(hit.tag=="Player")
   		hit.transform.parent = null;
}



function Update()
{

 	GetComponent.<Rigidbody>().MovePosition(Vector3(transform.position.x,transform.position.y+(5*Time.deltaTime),transform.position.z));


}