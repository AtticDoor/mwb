using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public partial class TestDevESCAPE : MonoBehaviour
{
    public virtual void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public virtual void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("TestDevelopmentMenu");
        }
    }

}