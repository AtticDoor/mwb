
 #pragma strict
class RotateImageFlappy extends RotateImage{

var WingCollider:GameObject;
function ExtraUpdate()
{   
	if(WingCollider==null)
		return;
       
    switch(frame[index])
    {
    case 0:
      WingCollider.transform.localScale.x=-3.36187059;
      break;
    case 1:
      WingCollider.transform.localScale.x=-3.36187059;;
      break;
    case 2:
      WingCollider.transform.localScale.x=-1.9755762;
      break;
    case 3:
      WingCollider.transform.localScale.x=0;
      break;
    case 4:
      WingCollider.transform.localScale.x=-1.59157908;
      break;
    case 5:
      WingCollider.transform.localScale.x=-3.36187059;
      break;
    }  
      
  
}



//var frame=[0,1,2,3,4,5];
//var frame:int[]	;//=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,5];

function ExtraStart()
{
	frame=[0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,3,3,3,4,5];
	//frame=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,5];
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
