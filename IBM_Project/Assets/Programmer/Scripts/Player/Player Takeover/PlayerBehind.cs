using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehind : MonoBehaviour
{
    private Vector3 playerPos;
    private Vector3 enemyPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos =  GameObject.FindGameObjectWithTag("Player").transform.position;
        enemyPos = this.gameObject.transform.position;


    }
}
