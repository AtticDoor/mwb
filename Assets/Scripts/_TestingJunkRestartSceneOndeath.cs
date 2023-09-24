using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class _TestingJunkRestartSceneOndeath : MonoBehaviour
{
    public string curLevel;
    public virtual void Start()
    {
        MainScript.curLevel = this.curLevel;
    }

    public virtual void Update()
    {
    }

}