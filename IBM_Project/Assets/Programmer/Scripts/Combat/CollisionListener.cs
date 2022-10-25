using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class CollisionListener : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject[] enemies;
    //get enemies to compile into array, check through array on collisions to determine which enemy hit the player. compare threat levels of multiple types of enemies dynamically with one script
    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject enemy in enemies)
        {
            if (other == enemy.GetComponent<CapsuleCollider>())
            {
                if(playerController.threatLevel < enemy.GetComponent<BotInfo>().threatLevel)
                {
                    Destroy(this);
                }
                else
                {
                    Destroy(enemy);
                }
            }
        }
    }


}

