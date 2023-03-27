using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{	
    public TextMeshProUGUI timerText; 
    public bool playing;
    public float timer = 180;
    public float timerOrigin;

    private void Update ()
    {
        if (!playing) return;
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("000");
    }
}