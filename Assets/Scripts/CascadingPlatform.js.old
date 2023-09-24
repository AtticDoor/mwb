#pragma strict
var lifeSpan:float=3;
var fadeTime:float=1;
var deadTime:float=3;

var StartPhase:int;
var StartTime:float;

 var fadingOut:boolean;
 var fadingIn:boolean;

private var FadeLevel:float;

function Start()//OnTriggerEnter (C:Collider) 
{

	StartTime=Time.time;	
	
	if(lifeSpan+fadeTime+deadTime==0)
	{
		lifeSpan=3;
		fadeTime=1;
		deadTime=3;
	}
	
	StartTime=0;
	InvokeRepeating("FadeOut",StartTime+lifeSpan        ,lifeSpan+fadeTime+fadeTime+deadTime);
	InvokeRepeating("FadeIn",StartTime+lifeSpan+fadeTime+deadTime                 ,lifeSpan+fadeTime+fadeTime+deadTime);	
	
}


function FadeIn()
{
	GetComponent.<Collider>().enabled=true;
	fadingIn=true;
	//FadeLevel=0;

}
function FadeOut()
{
	//collider.enabled=false;
	fadingOut=true;	
	//yield WaitForSeconds(fadeTime);
	//collider.enabled=false;
	//FadeLevel=1;

}

function Update()
{
	if (fadingIn)
	{
		FadeLevel+=Time.deltaTime;
		transform.GetComponent.<Renderer>().material.color.a=FadeLevel;
		if(transform.GetComponent.<Renderer>().material.color.a>=1)
		{
			transform.GetComponent.<Renderer>().material.color.a=1;
			//collider.enabled=true;
			fadingIn=false;	
		}		
	}
	else if (fadingOut)
	{
		FadeLevel-=Time.deltaTime;
		transform.GetComponent.<Renderer>().material.color.a=FadeLevel;
		if(transform.GetComponent.<Renderer>().material.color.a<=0)
		{
			transform.GetComponent.<Renderer>().material.color.a=0;
			
			GetComponent.<Collider>().enabled=false;	
			fadingOut=false;	
		}		
	}
	
	
}



/*

var phase:int;
function UpdateXXXXXXXXXXXXX()
{
	if (phase>=5)
	{
		phase=1;
		return;
	}


	if (dying)
	{
		transform.renderer.material.color.a*=1-Time.deltaTime;
	}
	else if(StartTime+delay<=Time.time)
	{
		DestroyPlatform();
	}
}

function DestroyPlatform()
{
	dying=true;
	yield WaitForSeconds(1);
	collider.active=false;

	Destroy(gameObject);
}

*/