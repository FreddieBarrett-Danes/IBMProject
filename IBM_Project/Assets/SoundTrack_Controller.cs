using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack_Controller : MonoBehaviour
{
    //[SerializeField]
    //bool Ship1;
    //[SerializeField]
    //bool Ship2;
    //[SerializeField]
    //bool Ship3;
    //[SerializeField]
    //bool Ship4;
    //[SerializeField]
    //bool Ship5;

    [SerializeField]
    private AudioSource safeState;
    [SerializeField]
    private AudioSource alertState;
    [SerializeField]
    private AudioSource huntedState;
    [SerializeField]
    private AudioSource minigameState;
    [SerializeField]
    private AudioSource quizState;
    
    private GameController gC;
    private bool playOnce = false;
    // Start is called before the first frame update
    void Awake()
    {
        safeState = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
        alertState = gameObject.transform.GetChild(1).GetComponent<AudioSource>();
        huntedState = gameObject.transform.GetChild(2).GetComponent<AudioSource>();
        minigameState = gameObject.transform.GetChild(3).GetComponent<AudioSource>();
        quizState = gameObject.transform.GetChild(4).GetComponent<AudioSource>();

        gC = gameObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gC.PlayerStatus == GameController.Status.SAFE && !gC.inMinigame && !gC.inQuiz)
        {
            Debug.Log("player safe");
            safeState.mute = false;
            alertState.mute = true;
            huntedState.mute = true;
            minigameState.mute = true;

        }
        else if (gC.PlayerStatus == GameController.Status.ALERTED && !gC.inMinigame && !gC.inQuiz)
        {
            Debug.Log("player alert");
            safeState.mute = true;
            alertState.mute = false;
            huntedState.mute = true;
            minigameState.mute = true;

        }
        else if (gC.PlayerStatus == GameController.Status.HUNTED && !gC.inMinigame && !gC.inQuiz)
        {
            Debug.Log("player hunted");
            safeState.mute = true;
            alertState.mute = true;
            huntedState.mute = false;
            minigameState.mute = true;

        }
        else if(gC.inMinigame && !gC.inQuiz)
        {
            safeState.mute = true;
            alertState.mute = true;
            huntedState.mute = true;
            minigameState.mute = false;

        }
        else if(gC.inQuiz)
        {
            safeState.mute = true;
            alertState.mute = true;
            huntedState.mute = true;
            minigameState.mute = true;
            if (!playOnce)
            {
                quizState.Play();
                playOnce = true;
            }
        }

    }
}
