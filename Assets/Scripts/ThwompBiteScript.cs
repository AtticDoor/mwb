using UnityEngine;
using System.Collections;

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
        this.BotClosedY = this.Bot.transform.position.y;
        this.TopClosedY = this.Top.transform.position.y;
    }

    public virtual void Update()
    {
        if (this.closing)
        {

            {
                float _184 = this.Bot.transform.position.y + (Time.deltaTime * 6);
                Vector3 _185 = this.Bot.transform.position;
                _185.y = _184;
                this.Bot.transform.position = _185;
            }

            {
                float _186 = this.Top.transform.position.y - (Time.deltaTime * 6);
                Vector3 _187 = this.Top.transform.position;
                _187.y = _186;
                this.Top.transform.position = _187;
            }
            if (this.Top.transform.position.y < this.TopClosedY)
            {
                this.closing = false;
            }
        }
        else
        {

            {
                float _188 = this.Bot.transform.position.y - (Time.deltaTime * 3);
                Vector3 _189 = this.Bot.transform.position;
                _189.y = _188;
                this.Bot.transform.position = _189;
            }

            {
                float _190 = this.Top.transform.position.y + (Time.deltaTime * 3);
                Vector3 _191 = this.Top.transform.position;
                _191.y = _190;
                this.Top.transform.position = _191;
            }
            if (this.Top.transform.position.y > 10)
            {
                this.closing = true;
            }
        }
    }

}