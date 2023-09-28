using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public partial class TimerGUI : MonoBehaviour
{
    public virtual void Awake()
    {
        if (MainScript.timer == -100)
            MainScript.timer = 10000;  //only for playing the scene without menu.

        if (MainScript.timer <= 0)
            TimerGUI.GameOver();
    }

    public virtual void Update()
    {
        MainScript.timer = MainScript.timer - Time.deltaTime;
    }

    public static int minutes;
    public static int seconds;
    public virtual void OnGUI()
    {
        TimerGUI.minutes = Mathf.FloorToInt(MainScript.timer / 60f);
        TimerGUI.seconds = Mathf.FloorToInt(MainScript.timer - (TimerGUI.minutes * 60));
        string niceTime = string.Format("{0:0}:{1:00}", TimerGUI.minutes, TimerGUI.seconds);
        GUI.depth = 10;
        GUI.Label(new Rect(10, 10, 250, 100), niceTime);
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void Death()
    {
        MainScript.timer -= 120;
        if (MainScript.timer < 0)
        {
            TimerGUI.GameOver();
        }
    }

}