class enemyPingPong extends EnemyScript{

	var LeftBoundary:GameObject;
	var RightBoundary:GameObject;

	private var left:float;
	private var right:float;

	var speed:float=1;
	function ExtraStart () {

		left=LeftBoundary.transform.position.x;
		right=RightBoundary.transform.position.x;
		
		
		MovingRight=false;
		transform.position.x=left;
		
	}


	private var MovingRight:boolean;

	function ExtraUpdate() {
	
		if(!On)return;
	

				     
		if (transform.position.x<left)		
			MovingRight=true;
		else if (transform.position.x>right)		
			MovingRight=false;
			
		if(MovingRight)	
			transform.position.x+=speed*Time.deltaTime;
		else
			transform.position.x-=speed*Time.deltaTime;
			
					
		
									    		     	
											    		     								    		     								    		     	
	}

}