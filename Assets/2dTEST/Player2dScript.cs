using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Player2dScript : MonoBehaviour
{
    public virtual void Start()
    {
    }

    public virtual void Update()//	rigidbody2D.AddForce(Vector3.up * 10 * Time.deltaTime);
    {
        if (Input.GetKey("a"))
        {

            {
                float _68 = this.transform.position.x - Time.deltaTime;
                Vector3 _69 = this.transform.position;
                _69.x = _68;
                this.transform.position = _69;
            }
        }
        if (Input.GetKey("d"))
        {

            {
                float _70 = this.transform.position.x + Time.deltaTime;
                Vector3 _71 = this.transform.position;
                _71.x = _70;
                this.transform.position = _71;
            }
        }
        if (Input.GetKeyDown("w"))
        {
            //	transform.GetComponent.<Rigidbody2D>().AddForce(Vector2(0,30),ForceMode.Force);
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
        }
    }

}