class HumanBulbScript extends EnemyScript{

	var OEMRotation:Quaternion;

	function ExtraStart () 
	{
		OEMRotation=transform.rotation;
	}

	var Alert:boolean=false;
	function Update () 
	{

		if(!Alert)
		{
			var hit:RaycastHit;
			var fwd = transform.TransformDirection (Vector3.left);
			if (Physics.Raycast (transform.position, fwd, hit)) 
			{
				if (hit.transform.gameObject.tag=="Player")
				{
					EnableColors(gameObject.tag,true);
					AlertEnemies();
					transform.rotation=Quaternion.Euler(0,90,0);
				}
			}
						
			fwd = transform.TransformDirection (Vector3.right);
			if (Physics.Raycast (transform.position, fwd, hit)) 
			{
				if (hit.transform.gameObject.tag=="Player")
				{
					EnableColors(gameObject.tag,true);
					AlertEnemies();
					transform.rotation=Quaternion.Euler(0,-90,0);
				}
			}			
		}
	}

	function EnableColors(color:String,t:boolean)
	{
		var Objects : GameObject[];
		Objects=GameObject.FindGameObjectsWithTag(color);
		for (var o in Objects)
		{
			o.GetComponent(EnemyScript).On=t;
		}
	}
	
	function AlertEnemies()
	{
		Alert=true;
		yield WaitForSeconds(3);
		transform.rotation=OEMRotation;
		Alert=false;	
	}
}