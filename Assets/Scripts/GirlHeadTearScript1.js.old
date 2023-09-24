#pragma strict

function Start () {
//transform.localScale.y=Random.Range(0,.45);
}

function Update () {


	if(transform.localScale.y<.45)	
	{
		transform.localScale.y+=Time.deltaTime/15;
		transform.localEulerAngles.z=Random.Range(-3,3);
	}
	else

		transform.position.y-=Time.deltaTime*5;

}




function OnCollisionEnter(c:Collision)
{
	Destroy(gameObject);

}