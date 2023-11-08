using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public partial class TestDevESCAPE : MonoBehaviour
{
    public virtual void Start()
    {
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
    }

    public virtual void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            SceneManager.LoadScene("TestDevelopmentMenu");
        }
    }

}