using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ahhhhh : MonoBehaviour
{

    public int avgFrameRate;
    //public Text display_Text;

    public void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        GetComponent<TextMeshProUGUI>().text = avgFrameRate.ToString() + " FPS";
    }

    public PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }
}
