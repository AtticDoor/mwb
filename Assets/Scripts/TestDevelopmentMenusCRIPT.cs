using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public partial class TestDevelopmentMenusCRIPT : MonoBehaviour
{
    public string[] list;
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnGUI()
    {
        int i = 0;
        while (i < list.Length)
        {
            if (GUI.Button(new Rect((i / 10) * 230, (i % 10) * 30, 230, 20), "Scene" + list[i]))
            {
                SceneManager.LoadScene("Scene" + list[i]);
            }
            i++;
        }
    }

}