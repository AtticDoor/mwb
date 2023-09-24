
 #pragma strict
class RotateImagePokey extends RotateImage{

function ExtraUpdate()
{   
	if (frame[index]>1)		
		transform.parent.gameObject.tag="AttackingEnemy";
	else transform.parent.gameObject.tag="Enemy";	
}

function ExtraStart()
{
	frame=[0,1,0,1,0,1,0,1,0,1,0,1,2,3,4,3,4,3,4,5];
}


//virtual 
function Update ()
{
 
    // Calculate index
    index  = (Time.time-StartTime) * framesPerSecond;
    // repeat when exhausting all frames
    index = (index % frame.length);//(uvAnimationTileX * uvAnimationTileY);
 
    // Size of every tile
    var size = Vector2 (1.0 / uvAnimationTileX, 1.0 / uvAnimationTileY);
 
    // split into horizontal and vertical index
    var uIndex = frame[index] % uvAnimationTileX;
    var vIndex = frame[index] / uvAnimationTileX;
 
 
    // build offset
    // v coordinate is the bottom of the image in opengl so we need to invert.
    var offset = Vector2 (uIndex * size.x, 1.0 - size.y - vIndex * size.y);
 
    GetComponent.<Renderer>().material.SetTextureOffset ("_MainTex", offset);
    GetComponent.<Renderer>().material.SetTextureScale ("_MainTex", size);

    ExtraUpdate();
}




}
