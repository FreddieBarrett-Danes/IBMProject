using UnityEngine;

public class MinigameController : MonoBehaviour
{
    private GameObject minigameHolder;
    public GameObject chosenMinigame;
    private int minigameCount;

    private GameController gameController;

    public bool completedMinigame = false;
    // Start is called before the first frame update
    void Start()
    {
        minigameHolder = GameObject.FindGameObjectWithTag("Minigames");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    public void StartMinigame()
    {
        if (!completedMinigame)
        {
            minigameCount = minigameHolder.transform.childCount;
            int randomMinigame = Mathf.RoundToInt(Random.Range(0, minigameCount - 1));
            chosenMinigame = minigameHolder.transform.GetChild(randomMinigame).gameObject;

            if (!chosenMinigame.activeSelf)
            {
                gameController.inMinigame = true;
                chosenMinigame.SetActive(true);
                gameObject.GetComponent<Renderer>().enabled = false;

            }
        }
    }
}
