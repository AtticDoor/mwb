#pragma strict


static var motion:boolean;


static function MoveObject (thisTransform : Transform, startPos : Vector3, endPos : Vector3, time : float) 
{
	var PO:EnemyScript=thisTransform.GetComponent("EnemyScript");
	
		


	motion=true;

	var i = 0.0;
	var rate = 1.0/time;
	
	while (i < 1.0) 
	{
	
	
		if ((PO!=null)&&(PO.On))
		{
		i += Time.deltaTime * rate;
		thisTransform.position = Vector3.Lerp(startPos, endPos, i);
	
			
		}	
		yield;
	}
}



     
static function ScaleObject (thisTransform : Transform, startPos : Vector3, endPos : Vector3, time : float) 
{
	var i = 0.0;
	var rate = 1.0/time;
	while (i < 1.0) 
	{
		i += Time.deltaTime * rate;
		thisTransform.localScale = Vector3.Lerp(startPos, endPos, i);
		yield;
	}
}	   

static function RotateObject (thisTransform : Transform, startPos : Vector3, endPos : Vector3, time : float) 
{
	var i = 0.0;
	var rate = 1.0/time;
	while (i < 1.0) 
	{
		i += Time.deltaTime * rate;
		thisTransform.localEulerAngles= Vector3.Lerp(startPos, endPos, i);
		yield;
	}
}	    