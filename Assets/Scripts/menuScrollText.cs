using UnityEngine;

[System.Serializable]
public partial class menuScrollText : MonoBehaviour
{
    public virtual void Start()
    {
    }

    public virtual void Update()
    {

        {
            float _166 = this.transform.position.y + (Time.deltaTime * 0.5f);
            Vector3 _167 = this.transform.position;
            _167.y = _166;
            this.transform.position = _167;
        }
    }

    /*
Once, there was a man.


A man who held a button.


A button, that once pressed,
would end the world.


And the man looked out at a world,
saw fear, violence, hatred, and suffering.

And the man, 
distraught and dismayed
by all he saw, 
withdrew and isolated himself 
from the world.

His mind built a tower around himself, 
further isolating himself from the horrors he saw.

And then he decided.
He would press the button.

The button that would end the world.

You must stop him.  You have 30 minutes.


*/
}