using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    private GameObject player;
    private GameObject minigameHolder;
    private GameObject level;

    public GameObject[] enemies;

    private int minigameCount;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        minigameHolder = GameObject.FindGameObjectWithTag("Minigames");
        level = GameObject.FindGameObjectWithTag("Level");
    }
    private void OnTriggerStay(Collider other)
    {
        if(other == player.GetComponent<CapsuleCollider>() && Input.GetKeyDown(KeyCode.E))
        {
            minigameCount = minigameHolder.transform.childCount;
            int randomMinigame = Mathf.RoundToInt(Random.Range(0, minigameCount - 1));
            GameObject chosenMinigame = minigameHolder.transform.GetChild(randomMinigame).gameObject;

            if(!chosenMinigame.active)
            {
                chosenMinigame.SetActive(true);
                level.SetActive(false);

            }

            foreach(GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            enemies = null;
        }
    }
}
