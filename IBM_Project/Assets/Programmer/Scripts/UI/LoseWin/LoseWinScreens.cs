using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

//using UnityEngine.SocialPlatforms.Impl;

public class LoseWinScreens : MonoBehaviour
{
    private Animator anim;
    public GameObject button;
    private RectTransform canvas;
    [SerializeField]
    private ScoreSystem scoreSystem;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();
        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();

    }
    private void OnGUI()
    {
        this.GetComponent<RectTransform>().sizeDelta = canvas.sizeDelta;
    }
    // Update is called once per frame
    void Update()
    {
        
        
        //Debug.Log(scoreSystem.name);
        if (!AnimatorIsPlaying())
        {
            button.SetActive(true);
            //scoreText.text = scoreSystem.score.ToString("Score: " + scoreSystem.score);
        }
    }
    bool AnimatorIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
