 #pragma strict
class RotateImageFloatyWings extends RotateImage{

function ExtraUpdate()
{	
	if (index==2)
		GameObject.Destroy(transform.parent.gameObject);
	
}

}