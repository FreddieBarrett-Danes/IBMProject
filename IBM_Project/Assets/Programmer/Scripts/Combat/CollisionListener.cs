using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{
    public PlayerController playerController;
    //get enemies to compile into array, check through array on collisions to determine which enemy hit the player. compare threat levels of multiple types of enemies dynamically with one script
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
