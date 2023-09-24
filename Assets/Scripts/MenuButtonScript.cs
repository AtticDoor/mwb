using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class MenuButtonScript : MonoBehaviour
{
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnMouseUp()
    {
         //set timer
        TimerGUI.timer = 60 * 20;
        Application.LoadLevel("Scene101");
    }

}