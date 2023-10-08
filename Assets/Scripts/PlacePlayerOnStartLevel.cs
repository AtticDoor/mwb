using UnityEngine;

[System.Serializable]
public partial class PlacePlayerOnStartLevel : MonoBehaviour
{
    public virtual void Start()
    {
        GameObject dest = GameObject.Find(MainScript.lastExit + MainScript.lastLevel);
        //	Debug.Log(MainScript.lastExit +MainScript.lastLevel+(dest!=null));
        if (dest != null)
        {

            {
                float _168 =  //Debug.Break();
                dest.transform.position.x;
                Vector3 _169 = transform.position;
                _169.x = _168;
                transform.position = _169;
            }

            {
                float _170 = dest.transform.position.y;
                Vector3 _171 = transform.position;
                _171.y = _170;
                transform.position = _171;
            }
        }
        else
        {
            transform.position = GameObject.Find("PlayerStartPoint").transform.position;
        }
    }

}