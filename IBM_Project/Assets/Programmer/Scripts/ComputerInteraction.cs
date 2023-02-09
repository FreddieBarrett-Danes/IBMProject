using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.IO;
using Unity.VisualScripting;

public class ComputerInteraction : MonoBehaviour
{
    private GameObject player;


    public GameObject[] enemies;
    public GameObject chosenMinigame;

    private GameController gameController;
    private MinigameController miniController;


    private bool isTouching;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)).GameObject();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        miniController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
    }

    void Update()
    {
        if (isTouching && Input.GetKey(KeyCode.E))
        {
            if (!miniController.completedMinigame)
            {
                miniController.StartMazeMinigame();
            }

        }
        if (miniController.completedMinigame)
        {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            gameObject.GetComponent<Renderer>().enabled = true;
            enemies = null;
            miniController.completedMinigame = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isTouching = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        /*if(other == player.GetComponent<CapsuleCollider>() && Input.GetKey(KeyCode.E))
        {
            if (!completedMinigame)
            {
                minigameCount = minigameHolder.transform.childCount;
                int randomMinigame = Mathf.RoundToInt(Random.Range(0, minigameCount - 1));
                chosenMinigame = minigameHolder.transform.GetChild(randomMinigame).gameObject;

                if (!chosenMinigame.active)
                {
                    gameController.inMinigame = true;

                }
            }
            else
            {
                foreach (GameObject enemy in enemies)
                {
                    Destroy(enemy);
                }
                enemies = null;
                completedMinigame = false;
            }

        }*/
    }
}
