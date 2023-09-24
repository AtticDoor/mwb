class CentipedMotionScript extends EnemyScript{

//#pragma strict
var MovementList:Transform[];
var delayBetweenMovesThenSpeed:float[];

var AnimatedObject:GameObject;

function ExtraStart () 
{	
	ExtraStart2 () ;
}


function ExtraStart2 () 
{
	for (var i:int=0;i<MovementList.length; i+=2)
	{
		while(!On){ yield;}// AnimatedObject.transform.animation.Speed=0;}//while(!OnOff)//while((PO!=null)&&(PO.Paused)){ yield; }
		//AnimatedObject.transform.animation.Speed=1;
		yield WaitForSeconds(delayBetweenMovesThenSpeed[i]);
		while(!On){ yield; }//while(!OnOff)//while ((PO!=null)&&(PO.Paused)){ yield; }
		MoveCentipede(MovementList[i].position,MovementList[i+1].position,delayBetweenMovesThenSpeed[i+1]);
		while(!On){ yield; }//while(!OnOff)//while ((PO!=null)&&(PO.Paused)){ yield; }
		yield WaitForSeconds(delayBetweenMovesThenSpeed[i+1]);	
		while(!On){ yield; }//while ((PO!=null)&&(PO.Paused)){ yield; }
	}
}


function MoveCentipede(startPos:Vector3,endPos:Vector3,t:float)
{
	var _direction:Vector3 = (startPos - endPos).normalized;

	var _lookRotation:Quaternion = Quaternion.LookRotation(_direction,Vector3(0,0,1));
	
	transform.rotation = _lookRotation;
	LerpObject.MoveObject(transform,startPos,endPos,t);
}




}