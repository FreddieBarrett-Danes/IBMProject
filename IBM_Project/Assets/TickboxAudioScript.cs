using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickboxAudioScript : MonoBehaviour
{
    public GameObject prefabToSpawn;
    private bool lastFrame;
    private Toggle toggle;
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    void Update()
    {
        if(lastFrame != toggle.isOn)
        {
            GameObject audio = Instantiate(prefabToSpawn, transform.position, transform.rotation);
            Destroy(audio, 3f);
        }

        lastFrame = toggle.isOn;
    }
}
