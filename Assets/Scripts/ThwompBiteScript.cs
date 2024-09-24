using UnityEngine;

[System.Serializable]
public partial class ThwompBiteScript : MonoBehaviour
{
    public float TopClosedY;
    public float BotClosedY;
    public bool closing;
    public GameObject Top;
    public GameObject Bot;
    public virtual void Start()
    {
        BotClosedY = Bot.transform.position.y;
        TopClosedY = Top.transform.position.y;
    }

    public virtual void Update()
    {
        if (closing)
        {
            {
                Vector3 newPos = Bot.transform.position;
                newPos.y = Bot.transform.position.y + (Time.deltaTime * 6);
                Bot.transform.position = newPos;
            }

            {
                Vector3 newPos = Top.transform.position;
                newPos.y = Top.transform.position.y - (Time.deltaTime * 6);
                Top.transform.position = newPos;
            }
            if (Top.transform.position.y < TopClosedY)
                closing = false;
        }
        else
        {
            {
                Vector3 newPos = Bot.transform.position;
                newPos.y = Bot.transform.position.y - (Time.deltaTime * 3);
                Bot.transform.position = newPos;
            }

            {
                Vector3 newPos = Top.transform.position;
                newPos.y = Top.transform.position.y + (Time.deltaTime * 3);
                Top.transform.position = newPos;
            }
            if (Top.transform.position.y > 10)
            {
                closing = true;
            }
        }
    }

}