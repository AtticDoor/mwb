#pragma strict


var color:String;
var AwakeOnStart:boolean;
var LookDirection:String;
var ActivateOnPlayerProximity:boolean;
var AwakeOnSamePlane:boolean;

var walkAndPause:boolean;
var Stationary:boolean;
var Erratic:boolean;;



var LeftBoundary:GameObject;
var RightBoundary:GameObject;

private var left:float;
private var right:float;


function OnTriggerEnter(c:Collider)
{
//	Debug.Log("FDFDSFDSFSDFDSFSDFDSGDSGSDGDSGDSGDSGDSGDSGDGDSGSGDSGSDGG");

	if (c.gameObject.tag=="Player")
	{
		KillPlayer(c.gameObject);
		//fadeOut();	
		 Time.timeScale = 0;
		c.transform.position=GameObject.Find("PlayerStartPoint").transform.position;
		

		
		
	//	yield WaitForSeconds(2);
		Time.timeScale = 1.0;
		//c.transform.position.y=300;
		//c.renderer.enabled=true;
		//Application.LoadLevel("Scene2");
			
	}

}

function KillPlayer(g:GameObject)
{
//g.renderer.enabled=false;

	//GameObject.Destroy(g);

//	g.GetComponent("Bip001 Pelvis")


}



function Start () {


	if (Stationary)
		pauseMotion=true;
	if (Erratic)
		Invoke("ErraticPause",Random.Range(.5,4));
/*/
	left=LeftBoundary.transform.position.x;
	right=RightBoundary.transform.position.x;
	
	dist=right-left;/*/

}



private var movingLeft:boolean=false;

private var pauseMotion:boolean=false;

function Update() 
{
	if(pauseMotion) return;
	
	
	if ((movingLeft)&&(transform.position.x<LeftBoundary.transform.position.x))
	{
		movingLeft=false;
		if(walkAndPause)
		{
			pauseMotion=true;
			Invoke ("TogglePauseMotion",1);
		}
	
	}
	else if ((!movingLeft)&&(transform.position.x>RightBoundary.transform.position.x))
	{
		movingLeft=true;
		if(walkAndPause)
		{
			pauseMotion=true;
			Invoke ("TogglePauseMotion",1);
		}	
	}
	
	else if (movingLeft)
		transform.position.x-=1.5*Time.deltaTime;
	else transform.position.x+=1.5*Time.deltaTime;
}





function TogglePauseMotion()
{
	pauseMotion=!pauseMotion;
}


function ErraticPause()
{
	TogglePauseMotion();
		Invoke("ErraticPause",Random.Range(.5,4));
}




