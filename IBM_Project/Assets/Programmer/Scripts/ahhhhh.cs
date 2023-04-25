using TMPro;
using UnityEngine;

public class Ahhhhh : MonoBehaviour
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


    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
