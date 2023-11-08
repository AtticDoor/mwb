using UnityEngine;

[System.Serializable]
public partial class SelectableObjectScript : MonoBehaviour
{
    public virtual void OnMouseUp()
    {
        MainScript.Selected = gameObject;
        if (MainScript.Selected.tag != "Boundary")
        {
            while (MainScript.Selected.transform.parent != null)
            {
                MainScript.Selected = MainScript.Selected.transform.parent.gameObject;
            }
        }
    }

}