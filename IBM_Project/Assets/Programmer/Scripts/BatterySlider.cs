using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatterySlider : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] float batteryCharge;
    public Slider slider;
    
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        batteryCharge = playerController.controlTimer;
        slider.value = batteryCharge;
    }
}
