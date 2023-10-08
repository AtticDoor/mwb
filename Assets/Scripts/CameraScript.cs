using UnityEngine;

[System.Serializable]
public partial class CameraScript : MonoBehaviour
{
    public GameObject Player;
    public Material yellowMat;
    public Material redMat;
    public Material blueMat;
    public bool verticalCam;
    public float lowPoint;
    public float highPoint;
    public float LeftPoint;
    public float RightPoint;
    public virtual void Awake()
    {
        MainScript.yellow = yellowMat;
        MainScript.red = redMat;
        MainScript.blue = blueMat;
    }

    public virtual void Start()
    {
        Player = GameObject.Find("Player");
    }

    public bool editable;
    public virtual void Update()
    {
        if (ThirdPersonController.lockCameraTimer > 0)
        {
            ThirdPersonController.lockCameraTimer = ThirdPersonController.lockCameraTimer - Time.deltaTime;
        }
        if (ThirdPersonController.lockCameraTimer > 0)
        {
            return;
        }
        if (editable)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                MainScript.EditMode = !MainScript.EditMode;
                GameObject.Find("bgShieldPlane").GetComponent<Renderer>().enabled = !MainScript.EditMode;
            }
            if (Player != null)
            {
                ThirdPersonController tpc = (ThirdPersonController)Player.GetComponent("ThirdPersonController");
                tpc.enabled = !MainScript.EditMode;
            }
            if (MainScript.EditMode)
            {
                //camera controls
                if (Input.GetKey("up"))
                {

                    {
                        float _90 = transform.position.y + (1 * Time.deltaTime);
                        Vector3 _91 = transform.position;
                        _91.y = _90;
                        transform.position = _91;
                    }
                }
                if (Input.GetKey("down"))
                {

                    {
                        float _92 = transform.position.y - (1 * Time.deltaTime);
                        Vector3 _93 = transform.position;
                        _93.y = _92;
                        transform.position = _93;
                    }
                }
                if (Input.GetKey("left"))
                {

                    {
                        float _94 = transform.position.x - (1 * Time.deltaTime);
                        Vector3 _95 = transform.position;
                        _95.x = _94;
                        transform.position = _95;
                    }
                }
                if (Input.GetKey("right"))
                {

                    {
                        float _96 = transform.position.x + (1 * Time.deltaTime);
                        Vector3 _97 = transform.position;
                        _97.x = _96;
                        transform.position = _97;
                    }
                }
                if (Input.GetKey("-"))
                {
                    transform.GetComponent<Camera>().orthographicSize = transform.GetComponent<Camera>().orthographicSize - (1 * Time.deltaTime);
                }
                if (Input.GetKey("="))
                {
                    transform.GetComponent<Camera>().orthographicSize = transform.GetComponent<Camera>().orthographicSize + (1 * Time.deltaTime);
                }
                if (MainScript.Selected != null)
                {
                    //delete	
                    if (Input.GetKey("x"))
                    {
                        GameObject.Destroy(MainScript.Selected);
                    }
                    if (Input.GetKey("w"))
                    {

                        {
                            float _98 = MainScript.Selected.transform.position.y + (0.2f * Time.deltaTime);
                            Vector3 _99 = MainScript.Selected.transform.position;
                            _99.y = _98;
                            MainScript.Selected.transform.position = _99;
                        }
                    }
                    if (Input.GetKey("s"))
                    {

                        {
                            float _100 = MainScript.Selected.transform.position.y - (0.2f * Time.deltaTime);
                            Vector3 _101 = MainScript.Selected.transform.position;
                            _101.y = _100;
                            MainScript.Selected.transform.position = _101;
                        }
                    }
                    if (Input.GetKey("a"))
                    {

                        {
                            float _102 = MainScript.Selected.transform.position.x + (0.2f * Time.deltaTime);
                            Vector3 _103 = MainScript.Selected.transform.position;
                            _103.x = _102;
                            MainScript.Selected.transform.position = _103;
                        }
                    }
                    if (Input.GetKey("d"))
                    {

                        {
                            float _104 = MainScript.Selected.transform.position.x - (0.2f * Time.deltaTime);
                            Vector3 _105 = MainScript.Selected.transform.position;
                            _105.x = _104;
                            MainScript.Selected.transform.position = _105;
                        }
                    }
                    if (Input.GetKey("t"))
                    {

                        {
                            float _106 = MainScript.Selected.transform.localScale.y + (0.2f * Time.deltaTime);
                            Vector3 _107 = MainScript.Selected.transform.localScale;
                            _107.y = _106;
                            MainScript.Selected.transform.localScale = _107;
                        }
                    }
                    if (Input.GetKey("g"))
                    {

                        {
                            float _108 = MainScript.Selected.transform.localScale.y - (0.2f * Time.deltaTime);
                            Vector3 _109 = MainScript.Selected.transform.localScale;
                            _109.y = _108;
                            MainScript.Selected.transform.localScale = _109;
                        }
                    }
                    if (Input.GetKey("f"))
                    {

                        {
                            float _110 = MainScript.Selected.transform.localScale.x + (0.2f * Time.deltaTime);
                            Vector3 _111 = MainScript.Selected.transform.localScale;
                            _111.x = _110;
                            MainScript.Selected.transform.localScale = _111;
                        }
                    }
                    if (Input.GetKey("h"))
                    {

                        {
                            float _112 = MainScript.Selected.transform.localScale.x - (0.2f * Time.deltaTime);
                            Vector3 _113 = MainScript.Selected.transform.localScale;
                            _113.x = _112;
                            MainScript.Selected.transform.localScale = _113;
                        }
                    }
                }
                return;
            }
        }
        if (Player != null)
        {
            if ((LeftPoint != 0) && (RightPoint != 0))
            {
                if (Player.transform.position.x <= LeftPoint)
                {

                    {
                        float _114 = LeftPoint;
                        Vector3 _115 = transform.position;
                        _115.x = _114;
                        transform.position = _115;
                    }
                }
                else
                {
                    if (Player.transform.position.x >= RightPoint)
                    {

                        {
                            float _116 = RightPoint;
                            Vector3 _117 = transform.position;
                            _117.x = _116;
                            transform.position = _117;
                        }
                    }
                    else
                    {

                        {
                            float _118 = Player.transform.position.x;
                            Vector3 _119 = transform.position;
                            _119.x = _118;
                            transform.position = _119;
                        }
                    }
                }
            }
            else
            {

                {
                    float _120 = Player.transform.position.x;
                    Vector3 _121 = transform.position;
                    _121.x = _120;
                    transform.position = _121;
                }
            }
            if (verticalCam)
            {
                if (Player.transform.position.y <= lowPoint)
                {

                    {
                        float _122 = lowPoint;
                        Vector3 _123 = transform.position;
                        _123.y = _122;
                        transform.position = _123;
                    }
                }
                else
                {
                    if (Player.transform.position.y >= highPoint)
                    {

                        {
                            float _124 = highPoint;
                            Vector3 _125 = transform.position;
                            _125.y = _124;
                            transform.position = _125;
                        }
                    }
                    else
                    {

                        {
                            float _126 = Player.transform.position.y;
                            Vector3 _127 = transform.position;
                            _127.y = _126;
                            transform.position = _127;
                        }
                    }
                }
            }
        }
    }

}