#pragma strict

var speed:float;

function Start () 
{
speed=Random.Range(-.1,.1);
transform.localScale=Vector3(0,0,1);

}

function Update () {
	
	transform.Rotate(0,0,speed);
	
	var amt=Time.deltaTime*3;
	transform.localScale+=Vector3(amt,amt,0);
	
	
	if (transform.localScale.x>5)
		GetComponent.<Renderer>().material.color.a-=amt/8;
	if(GetComponent.<Renderer>().material.color.a<0)
		Destroy(gameObject);


}