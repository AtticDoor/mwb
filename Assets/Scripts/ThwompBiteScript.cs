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
                float _184 = Bot.transform.position.y + (Time.deltaTime * 6);
                Vector3 _185 = Bot.transform.position;
                _185.y = _184;
                Bot.transform.position = _185;
            }

            {
                float _186 = Top.transform.position.y - (Time.deltaTime * 6);
                Vector3 _187 = Top.transform.position;
                _187.y = _186;
                Top.transform.position = _187;
            }
            if (Top.transform.position.y < TopClosedY)
            {
                closing = false;
            }
        }
        else
        {

            {
                float _188 = Bot.transform.position.y - (Time.deltaTime * 3);
                Vector3 _189 = Bot.transform.position;
                _189.y = _188;
                Bot.transform.position = _189;
            }

            {
                float _190 = Top.transform.position.y + (Time.deltaTime * 3);
                Vector3 _191 = Top.transform.position;
                _191.y = _190;
                Top.transform.position = _191;
            }
            if (Top.transform.position.y > 10)
            {
                closing = true;
            }
        }
    }

}