using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    private GameObject player;
    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void OnTriggerStay(Collider other)
    {
        if(other == player.GetComponent<CapsuleCollider>() && Input.GetKeyDown(KeyCode.E))
        {
            foreach(GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            enemies = null;
        }
    }
}
