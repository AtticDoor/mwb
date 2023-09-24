class SawBladeScript extends EnemyScript{
var speed:int=200;


function FixedUpdate () {
	if (On)
		transform.Rotate(0,0,speed*Time.deltaTime);
}



}