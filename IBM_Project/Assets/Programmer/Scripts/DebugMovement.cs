using UnityEngine;

public class DebugMovement : MonoBehaviour
{
    bool right;
    void Update()
    {
        if(this.transform.position.x > 5)
        {
            right = false;
        }
        else if(this.transform.position.x < -5)
        {
            right = true;
        }

        if (right == true)
            this.transform.position = new Vector3(this.transform.position.x + Time.deltaTime * 7f,0,0);
        else
            this.transform.position = new Vector3(this.transform.position.x + Time.deltaTime * -7f,0,0);
    }
}
