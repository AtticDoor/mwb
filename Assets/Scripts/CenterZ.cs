using UnityEngine;

[System.Serializable]
public partial class CenterZ : MonoBehaviour
{
    public virtual void Update()//Update2();
    {

        {
            int _136 = 0;
            Vector3 _137 = this.transform.position;
            _137.z = _136;
            this.transform.position = _137;
        }
    }

    public virtual void Update2()
    {
        CharacterController controller = (CharacterController)this.GetComponent(typeof(CharacterController));
        if (controller.isGrounded)
        {
            MonoBehaviour.print("We are grounded");
        }
        else
        {
            MonoBehaviour.print("Not Grounded");
        }
    }

}