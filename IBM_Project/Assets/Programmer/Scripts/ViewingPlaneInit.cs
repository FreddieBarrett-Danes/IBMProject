using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewingPlaneInit : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Vector3 temp = new Vector3(transform.position.x, mainCamera.transform.position.y + 1, transform.position.z);
        transform.position = temp;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 1, mainCamera.transform.position.z);
        transform.position = temp;
    }
}
