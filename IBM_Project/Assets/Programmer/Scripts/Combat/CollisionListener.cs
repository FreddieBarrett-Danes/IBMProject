using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class CollisionListener : MonoBehaviour
{
    private GameObject player;
    public GameObject[] enemies;
    //get enemies to compile into array, check through array on collisions to determine which enemy hit the player. compare threat levels of multiple types of enemies dynamically with one script
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == player.GetComponent<CapsuleCollider>())
        {
            if (player.GetComponent<PlayerController>().threatLevel < this.gameObject.GetComponentInParent<BotInfo>().threatLevel)
            {
                Destroy(player);
            }
            else if (player.GetComponent<PlayerController>().threatLevel > this.gameObject.GetComponentInParent<BotInfo>().threatLevel)
            {
                Destroy(this.gameObject.transform.parent.gameObject);
            }
        }

/*        foreach (GameObject enemy in enemies)
        {
            if (other == enemy.GetComponent<CapsuleCollider>())
            {
                if(player.GetComponent<PlayerController>().threatLevel < enemy.GetComponent<BotInfo>().threatLevel)
                {
                    Destroy(player);
                }
                else if (player.GetComponent<PlayerController>().threatLevel > enemy.GetComponent<BotInfo>().threatLevel)
                {
                    Destroy(enemy);
                }
            }
        }*/
    }


}

