using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehind : MonoBehaviour
{
    private Transform other;
    // Start is called before the first frame update
    void Start()
    {
        other = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(other != null)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = other.position - transform.position;

            if(Vector3.Dot(forward, toOther) < 0)
            {
                Debug.Log("Player is behind enemy");
            }
            else
            {
                Debug.Log("Player is infront of enemy");
            }
        }
    }
}
