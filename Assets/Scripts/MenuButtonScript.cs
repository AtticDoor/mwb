using UnityEngine;
using UnityEngine.SceneManagement;


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
        MainScript.timer = 60 * 20;
        SceneManager.LoadScene("Scene101");
    }

}