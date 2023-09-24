#pragma strict

var phase:int;
var src:GameObject;
var dest:GameObject;
var startTime:float;

function Start () 
{
	startTime=Time.time;
	Debug.Log(MainScript.curLevel+" "+MainScript.lastLevel);
	phase=1;
	src=GameObject.Find("Scene"+MainScript.lastLevel+MainScript.curLevel);
	dest=GameObject.Find(MainScript.curLevel+"Scene"+MainScript.lastLevel);
	Camera.main.orthographicSize=.07;

	//transform.position.x=src.transform.position.x;
	//transform.position.y=src.transform.position.y;
	
	var srcPos:Vector3=src.transform.position;
	srcPos.z=Camera.main.transform.position.z;
	var destPos:Vector3=dest.transform.position;
	destPos.z=Camera.main.transform.position.z;
	
	LerpObject.MoveObject (Camera.main.transform, srcPos, destPos , 5);
}




function Update () 
{
	if (Time.time-startTime<2.5)
	{
		Camera.main.orthographicSize+=Time.deltaTime*2;
		if(Camera.main.orthographicSize>6)
			phase=3;
	}

	else// if (phase==3)
	{
		Camera.main.orthographicSize-=Time.deltaTime*2;
		if(Camera.main.orthographicSize<.07)
			Application.LoadLevel(MainScript.curLevel);
	}
}