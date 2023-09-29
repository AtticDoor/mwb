using UnityEngine;

[System.Serializable]
public partial class EditModeDisplayTransparentObject : MonoBehaviour
{
    public virtual void Update()
    {
        this.GetComponent<Renderer>().enabled = MainScript.EditMode;
    }

}