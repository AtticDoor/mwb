using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public partial class MenuButtonScript : MonoBehaviour
{
    public string[] SceneList = new string[]
    {
        "Scene5",
        "SceneBlimp",
        "SceneCentipedeTestLevel",
        "SceneCentipedeTestLevel2",
        "SceneFemaleCryingHead",
        "SceneFlyingCrow",
        "SceneGator",
        "SceneGearHead",
        "SceneHeart",
        "SceneNumbers",
        "SceneTestCascadingPlatforms",
        "SceneTestHeadOnWall",
        "SceneGreen",
        "SceneLevelDesign",
        "SceneLevelDesign2"
    };
    public virtual void Start()
    {
    }

    public string gameType;
    public virtual void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    public virtual void OnMouseUp()
    {
        MainScript.GameType = gameType;
        MainScript.timer = 60 * 20;

        if (gameType == "A")
            SceneManager.LoadScene("Scene101");



        else if (gameType == "B")
        {
            MainScript.SceneListNumber = 0;
            MainScript.SceneList = new string[SceneList.Length];
            for (int i = 0; i < SceneList.Length; i++)
            {
                MainScript.SceneList[i] = SceneList[i];
            }
            for (int i = 0; i < SceneList.Length; i++)
            {
                Debug.Log(SceneList[i]);
            }
            SceneManager.LoadScene(MainScript.SceneList[MainScript.SceneListNumber]);
        }
        //default
        else SceneManager.LoadScene("Scene101");

    }

}