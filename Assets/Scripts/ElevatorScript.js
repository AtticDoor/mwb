#pragma strict

var Codes:int[];

var Plane1:GameObject;
var Plane2:GameObject;
var Plane3:GameObject;

static var static1:boolean=false;
static var static2:boolean=false;
static var static3:boolean=false;




var levelName:String;

var DoorOpen:GameObject;

function Start()
{
	static1=false;
	static2=false;
	static3=false;
}

static function DoorSwitchOn(i:int)
{
	if (i==1)static1=true;
	if (i==2)static2=true;
	if (i==3)static3=true;
	
}

function Update()
{
	Plane1.active=static1;
	Plane2.active=static2;
	Plane3.active=static3;
	
	if(!DoorOpen.active)
	{
		for (var i:int=0;i<Codes.length;i++)
		{
			//Debug.Log(ElevatorCodes.TVCleared(Codes[i])+"   "+Codes.length+" i:"+i+"  CodesI:"+Codes[i]+" ");
			if(!ElevatorCodes.TVCleared(Codes[i]))
				return;
		}
	}
	OpenDoor();
}

function OpenDoor()
{
	DoorOpen.active=true;
}