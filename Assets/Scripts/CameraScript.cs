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
        MainScript.yellow = this.yellowMat;
        MainScript.red = this.redMat;
        MainScript.blue = this.blueMat;
    }

    public virtual void Start()
    {
        this.Player = GameObject.Find("Player");
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
        if (this.editable)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                MainScript.EditMode = !MainScript.EditMode;
                GameObject.Find("bgShieldPlane").GetComponent<Renderer>().enabled = !MainScript.EditMode;
            }
            if (this.Player != null)
            {
                ThirdPersonController tpc = (ThirdPersonController)this.Player.GetComponent("ThirdPersonController");
                tpc.enabled = !MainScript.EditMode;
            }
            if (MainScript.EditMode)
            {
                //camera controls
                if (Input.GetKey("up"))
                {

                    {
                        float _90 = this.transform.position.y + (1 * Time.deltaTime);
                        Vector3 _91 = this.transform.position;
                        _91.y = _90;
                        this.transform.position = _91;
                    }
                }
                if (Input.GetKey("down"))
                {

                    {
                        float _92 = this.transform.position.y - (1 * Time.deltaTime);
                        Vector3 _93 = this.transform.position;
                        _93.y = _92;
                        this.transform.position = _93;
                    }
                }
                if (Input.GetKey("left"))
                {

                    {
                        float _94 = this.transform.position.x - (1 * Time.deltaTime);
                        Vector3 _95 = this.transform.position;
                        _95.x = _94;
                        this.transform.position = _95;
                    }
                }
                if (Input.GetKey("right"))
                {

                    {
                        float _96 = this.transform.position.x + (1 * Time.deltaTime);
                        Vector3 _97 = this.transform.position;
                        _97.x = _96;
                        this.transform.position = _97;
                    }
                }
                if (Input.GetKey("-"))
                {
                    this.transform.GetComponent<Camera>().orthographicSize = this.transform.GetComponent<Camera>().orthographicSize - (1 * Time.deltaTime);
                }
                if (Input.GetKey("="))
                {
                    this.transform.GetComponent<Camera>().orthographicSize = this.transform.GetComponent<Camera>().orthographicSize + (1 * Time.deltaTime);
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
        if (this.Player != null)
        {
            if ((this.LeftPoint != 0) && (this.RightPoint != 0))
            {
                if (this.Player.transform.position.x <= this.LeftPoint)
                {

                    {
                        float _114 = this.LeftPoint;
                        Vector3 _115 = this.transform.position;
                        _115.x = _114;
                        this.transform.position = _115;
                    }
                }
                else
                {
                    if (this.Player.transform.position.x >= this.RightPoint)
                    {

                        {
                            float _116 = this.RightPoint;
                            Vector3 _117 = this.transform.position;
                            _117.x = _116;
                            this.transform.position = _117;
                        }
                    }
                    else
                    {

                        {
                            float _118 = this.Player.transform.position.x;
                            Vector3 _119 = this.transform.position;
                            _119.x = _118;
                            this.transform.position = _119;
                        }
                    }
                }
            }
            else
            {

                {
                    float _120 = this.Player.transform.position.x;
                    Vector3 _121 = this.transform.position;
                    _121.x = _120;
                    this.transform.position = _121;
                }
            }
            if (this.verticalCam)
            {
                if (this.Player.transform.position.y <= this.lowPoint)
                {

                    {
                        float _122 = this.lowPoint;
                        Vector3 _123 = this.transform.position;
                        _123.y = _122;
                        this.transform.position = _123;
                    }
                }
                else
                {
                    if (this.Player.transform.position.y >= this.highPoint)
                    {

                        {
                            float _124 = this.highPoint;
                            Vector3 _125 = this.transform.position;
                            _125.y = _124;
                            this.transform.position = _125;
                        }
                    }
                    else
                    {

                        {
                            float _126 = this.Player.transform.position.y;
                            Vector3 _127 = this.transform.position;
                            _127.y = _126;
                            this.transform.position = _127;
                        }
                    }
                }
            }
        }
    }

}