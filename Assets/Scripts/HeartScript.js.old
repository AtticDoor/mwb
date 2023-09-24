#pragma strict

var Swirl:GameObject;


private var  beatState:int;


function Start () {
beatState=0;
	InvokeRepeating("Beat",1,1);
	InvokeRepeating("StartSwirl",1,1);
}

function Update () {

	if (beatState!=0)
	{
		if(beatState==1)
		{
			transform.localScale*= 1+(Time.deltaTime*.5);
			if (transform.localScale.x>3.25)
				beatState=2;
		}
		if(beatState==2)
		{
			transform.localScale*= 1-(Time.deltaTime*.5);
			if (transform.localScale.x<3)
			{
				transform.localScale.x=3;
				transform.localScale.y=6;
				transform.localScale.z=1;
				beatState=0;
			}
		}
	
	}


}


function StartSwirl()
{
	Instantiate(Swirl,Vector3(transform.position.x,transform.position.y,transform.position.z+.1),transform.rotation);

}

function Beat()
{
	beatState=1;


}