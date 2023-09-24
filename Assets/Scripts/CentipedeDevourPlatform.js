#pragma strict

function OnTriggerEnter (C:Collider) 
{
	if(C.transform.name=="Centi")
	{
		DestroyPlatform();
	}
}



private var dying:boolean;
function Update()
{
	if (dying)
	{
		transform.GetComponent.<Renderer>().material.color.a*=1-Time.deltaTime;
	}
}

function DestroyPlatform()
{
	dying=true;
	yield WaitForSeconds(1);

	Destroy(gameObject);
}