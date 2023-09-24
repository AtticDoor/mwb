using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class TimerGUI : MonoBehaviour
{
    public static float timer;
    public virtual void Awake()
    {
        if (TimerGUI.timer <= 0)
        {
            TimerGUI.GameOver();
        }
    }

    public virtual void Update()
    {
        TimerGUI.timer = TimerGUI.timer - Time.deltaTime;
    }

    public static int minutes;
    public static int seconds;
    public virtual void OnGUI()
    {
        TimerGUI.minutes = Mathf.FloorToInt(TimerGUI.timer / 60f);
        TimerGUI.seconds = Mathf.FloorToInt(TimerGUI.timer - (TimerGUI.minutes * 60));
        string niceTime = string.Format("{0:0}:{1:00}", TimerGUI.minutes, TimerGUI.seconds);
        GUI.depth = 10;
        GUI.Label(new Rect(10, 10, 250, 100), niceTime);
    }

    public static void GameOver()
    {
        Application.LoadLevel("Menu");
    }

    public static void Death()
    {
        TimerGUI.timer = TimerGUI.timer - 120;
        if (TimerGUI.timer < 0)
        {
            TimerGUI.GameOver();
        }
    }

}