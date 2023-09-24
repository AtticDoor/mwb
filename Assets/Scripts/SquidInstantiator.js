#pragma strict
var Squid:GameObject;

function Start () {
InvokeRepeating("NewSquid",4,4);

}

function Update () {

}


function NewSquid()
{
	var g:GameObject=Instantiate (Squid,transform.position,transform.rotation);
	
	g.transform.position=Vector3(Random.Range(4,-6),-5,0);
	
	


}