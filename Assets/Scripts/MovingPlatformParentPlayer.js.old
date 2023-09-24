#pragma strict

function OnTriggerEnter (other : Collider) 
{
 	if(other.transform.name==("Player"))
 		other.transform.parent = gameObject.transform; 
 }
 function OnTriggerExit (other : Collider) 
 {
 	if(other.transform.name==("Player"))
  		other.transform.parent = null; 
 }