#pragma strict
var Jaw:GameObject;


function Start () {
 Invoke("Open",0);
}


function Open()
{
	LerpObject.RotateObject(Jaw.transform, Vector3(0,0,7.22),Vector3(0,0,-50),4);
	Invoke("Close",5);

}


function Close()
{
	LerpObject.RotateObject(Jaw.transform, Vector3(0,0,-50),Vector3(0,0,7.22),4);
 Invoke("Open",5);


}