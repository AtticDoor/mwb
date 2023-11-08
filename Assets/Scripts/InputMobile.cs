using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputMobile : MonoBehaviour
{
    public bool Left;
    // Start is called before the first frame update
    void Start()
    {
        MovingUp = false;
    }

    /*
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log("double click");
            Debug.Break();
        }
    }*/
    private void OnMouseDown()
    {
        Debug.Log("Clicked" + transform.name);
        StartCLick = Input.mousePosition;

        if (!Left)
            Jumping = true;


        if (Left)
        {
            initialPos = Input.mousePosition;

            clicked++;
            if (clicked == 1) clicktime = Time.time;

        }


    }
    Vector3 StartCLick;

    private void OnMouseUp()
    {
        if (Left)
        {
            Running = false;
        
            initialPos = Vector3.zero;
            MovingLeft = false;
            MovingRight = false;
            MovingUp = false;
        }

        if (!Left)
            Jumping = false;

    }


    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    bool DoubleClick()
    {
      /*  if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }*/
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            Running = true;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }
    public static bool MovingUp;
    public static bool MovingLeft;
    public static bool MovingRight;
    public static bool Jumping;
    public static bool Running;

    private Vector3 initialPos = Vector3.zero;

    private void Update()
    {

        if (Left)
        {
            if (!Running)
                Running = DoubleClick();

            if (initialPos != Vector3.zero)
            {
                Vector3 finalPos = Input.mousePosition;


                MovingUp= finalPos.y > initialPos.y;
                if (!MovingUp)
                {
                    MovingLeft = finalPos.x < initialPos.x;
                    MovingRight = finalPos.x > initialPos.x;
                }
            }
        }

    }

}
