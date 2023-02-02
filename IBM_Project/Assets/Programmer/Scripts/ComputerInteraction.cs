using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.IO;

public class ComputerInteraction : MonoBehaviour
{
    private GameObject player;
    private GameObject minigameHolder;
    private GameObject level;

    public GameObject[] enemies;
    public GameObject chosenMinigame;

    private GameController gameController;
    private int minigameCount;

    public bool completedMinigame = false;

    private bool isTouching;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        minigameHolder = GameObject.FindGameObjectWithTag("Minigames");
        level = GameObject.FindGameObjectWithTag("Level");
        gameController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
    }

    void Update()
    {
        if (isTouching && Input.GetKey(KeyCode.E))
        {
            if (!completedMinigame)
            {
                minigameCount = minigameHolder.transform.childCount;
                int randomMinigame = Mathf.RoundToInt(Random.Range(0, minigameCount - 1));
                chosenMinigame = minigameHolder.transform.GetChild(randomMinigame).gameObject;

                if (!chosenMinigame.active)
                {
                    gameController.inMinigame = true;
                    chosenMinigame.SetActive(true);
                    gameObject.GetComponent<Renderer>().enabled = false;

                }
            }

        }
        if (completedMinigame)
        {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            gameObject.GetComponent<Renderer>().enabled = true;
            enemies = null;
            completedMinigame = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == player.tag)
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == player.tag)
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
