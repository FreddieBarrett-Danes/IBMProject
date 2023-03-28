using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseWinScreens : MonoBehaviour
{
    private Animator anim;
    public GameObject button;
    private RectTransform canvas;
   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();
    }
    private void OnGUI()
    {
        this.GetComponent<RectTransform>().sizeDelta = canvas.sizeDelta;
    }
    // Update is called once per frame
    void Update()
    {
        if(!AnimatorIsPlaying())
        {
            button.SetActive(true);
        }
    }
    bool AnimatorIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}