using System;
using System.IO;
using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class EditModeScript : MonoBehaviour
{
    public bool LoadGUI;
    public virtual void Update()
    {
        if (Input.GetKeyUp("1"))
        {
            this.SaveX();
        }
        if (Input.GetKeyUp("2"))
        {
            this.LoadGUI = true;
        }
    }

    public virtual void Start()
    {
        this.fileName = "C:\\MWB\\MWB";
    }

    public string fileName;
    public virtual void SaveX()
    {
        int i = 0;
        int num = 0;
        string fileName2 = (this.fileName + num) + ".txt";
        while (File.Exists(fileName2))
        {
            num++;
            fileName2 = (this.fileName + num) + ".txt";
            Debug.Log(fileName2 + " already exists.");
        }
        //            return;
        StreamWriter sr = File.CreateText(fileName2);
        //sr.WriteLine ("This is my file.");
        //sr.WriteLine ("I can write ints {0} or floats {1}, and so on.",
        //  gameObject.transform.position.x, 4.2999f);
        GameObject[] gameObjs = ((GameObject[]) UnityEngine.Object.FindObjectsOfType(typeof(GameObject))) as GameObject[];
        //find all Walls            
        while (i < gameObjs.Length)
        {
            //if (gameObjs[i].name=="WALLS")
            if (gameObjs[i].transform.parent != null)//=="WALLS")
            {
                sr.WriteLine("{0},{1},{2},{3}", new object[] {gameObjs[i].name, gameObjs[i].transform.position.x, gameObjs[i].transform.position.y, gameObjs[i].transform.position.z});
            }
            i++;
        }
        sr.Close();
        Debug.Log("saved");
    }

    public virtual void Load(string f)
    {
        StreamReader sr = new StreamReader(f);
        string fileContents = sr.ReadToEnd();
        sr.Close();
        string[] lines = fileContents.Split(new char[] {"\n"[0]});
        foreach (string line in lines)
        {
            MonoBehaviour.print(line);
        }
    }

    public virtual void OnGUI()
    {
        if (!this.LoadGUI)
        {
            return;
        }
        int num = 0;
        while (num < 20)
        {
            //var num:int=0;
            string fileName2 = (this.fileName + num) + ".txt";
            Debug.Log(fileName2);
            if (File.Exists(fileName2))
            {
                if (GUI.Button(new Rect(num * 50, 400, 50, 50), num + ""))
                {
                    this.Load(fileName2);
                    this.LoadGUI = false;
                }
            }
            num++;
        }
        if (GUI.Button(new Rect(0, 600, 100, 100), "Cancel"))
        {
            this.LoadGUI = false;
        }
    }

    public EditModeScript()
    {
        this.fileName = "C:\\MWB\\MWB";
    }

}