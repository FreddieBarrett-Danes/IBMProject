using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [Range(0f, 1f), SerializeField]
    private float volume;

    void OnGUI()
    {
        volume = GetComponent<Slider>().value;
        AudioListener.volume = volume/10;
    }
}
