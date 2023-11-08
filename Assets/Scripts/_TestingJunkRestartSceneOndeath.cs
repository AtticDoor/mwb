using UnityEngine;

[System.Serializable]
public partial class _TestingJunkRestartSceneOndeath : MonoBehaviour
{
    public string curLevel;
    public virtual void Start()
    {
        MainScript.curLevel = curLevel;
    }

    public virtual void Update()
    {
    }

}