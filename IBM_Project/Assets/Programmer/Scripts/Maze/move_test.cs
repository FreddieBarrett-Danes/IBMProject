using UnityEngine;

public class move_test : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Cube Collision");
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Time.deltaTime, 0, 0);
        
    }
}
