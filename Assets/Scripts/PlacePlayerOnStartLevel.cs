using UnityEngine;

[System.Serializable]
public partial class PlacePlayerOnStartLevel : MonoBehaviour
{
    public virtual void Start()
    {
        GameObject dest = GameObject.Find(MainScript.lastExit + MainScript.lastLevel);
        if (dest != null)
        {
            Vector3 newPos = transform.position;
            newPos.x = dest.transform.position.x;
            newPos.y = dest.transform.position.y;
            transform.position = newPos;
        }
        else
        {
            transform.position = GameObject.Find("PlayerStartPoint").transform.position;
        }
    }

}