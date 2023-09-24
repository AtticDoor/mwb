#pragma strict

function Start () {

}

function Update () {

}




function OnTriggerEnter(c:Collider)
{
	if(c.transform.name=="ali")
	{
		var g:GameObject=c.gameObject;
		var a:aliScript=g.GetComponent("aliScript");
		
		a.Hooked=true;
		transform.parent.parent=g.transform;
	}	
}