using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject quizMaster;
    [SerializeField]
    private ReadTSV reader;

    public Vector3 debug;

    //OMG FOR THE LOVE OF GOD WE NEED TO CHANGE THIS LATER. UNLOAD THE SCENE OR SOMETHING. 
    private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        quizMaster = GameObject.FindGameObjectWithTag("QuizMaster");
        reader = quizMaster.GetComponent<ReadTSV>();

        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            reader.find = true;
            cam.GetComponent<Camera>().farClipPlane = 0.5f;
        }
    }
}
