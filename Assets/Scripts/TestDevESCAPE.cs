using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class TestDevESCAPE : MonoBehaviour
{
    public virtual void Start()
    {
        UnityEngine.Object.DontDestroyOnLoad(this.gameObject);
    }

    public virtual void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            Application.LoadLevel("TestDevelopmentMenu");
        }
    }

}