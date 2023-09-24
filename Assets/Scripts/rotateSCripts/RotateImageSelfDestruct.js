 #pragma strict
class RotateImageSelfDestruct extends RotateImage{

 


function ExtraUpdate()
{
	if (index==numFrames-1)
		GameObject.Destroy(gameObject);
}
}