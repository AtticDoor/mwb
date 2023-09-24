#pragma strict

import System;
import System.IO;


var LoadGUI:boolean;

function Update () {
	if(Input.GetKeyUp("1"))	
		SaveX();
	if(Input.GetKeyUp("2"))	
		LoadGUI=true;
	

}


function Start()
{
	fileName = "C:\\MWB\\MWB";
}

 
var  fileName = "C:\\MWB\\MWB";
 
function SaveX()
{	
#if !UNITY_WEBPLAYER	

		var num:int=0;
		var fileName2=fileName+num+".txt";
        while (File.Exists(fileName2))
        {
        	num++;
        	fileName2=fileName+num+".txt";
            Debug.Log(fileName2+" already exists.");
//            return;
	
        }
        var sr = File.CreateText(fileName2);
        //sr.WriteLine ("This is my file.");
        //sr.WriteLine ("I can write ints {0} or floats {1}, and so on.",
          //  gameObject.transform.position.x, 4.2999f);
            
            
		var gameObjs : GameObject[] = FindObjectsOfType(GameObject) as GameObject[];            
            
	//find all Walls            
		for (var i:int;i<gameObjs.length; i++)
		
		
		//if (gameObjs[i].name=="WALLS")
        if (gameObjs[i].transform.parent!=null)//=="WALLS")
        
        sr.WriteLine ("{0},{1},{2},{3}"
       		,gameObjs[i].name,gameObjs[i].transform.position.x, gameObjs[i].transform.position.y, gameObjs[i].transform.position.z);		
            
            
            
            
        sr.Close();
        
        Debug.Log("saved");
        #endif
}

     
function Load (f:String) {


    var sr = new StreamReader(f);
    var fileContents = sr.ReadToEnd();
    sr.Close();
     
    var lines = fileContents.Split("\n"[0]);
    for (line in lines) {
    print (line);
    }
}

function OnGUI()
{
	if(!LoadGUI) return;



		for (var num:int=0;num<20;num++)
		{//var num:int=0;
			var fileName2=fileName+num+".txt";
			Debug.Log(fileName2);
	        if (File.Exists(fileName2))
    	    {
    	    	if (GUI.Button(Rect((num*50),400,50,50),num+""))
    	    	{
        			Load(fileName2);
        			LoadGUI=false;
        		}
        	}
        }

if (GUI.Button(Rect(0,600,100,100),"Cancel"))
    	    	{
        			LoadGUI=false;
        		}
}