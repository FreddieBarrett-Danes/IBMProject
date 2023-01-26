using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoSizeTest : MonoBehaviour
{
    public float size;
    public bool isAuto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        size = GetComponent<TextMeshProUGUI>().fontSize;
        isAuto = GetComponent<TextMeshProUGUI>().enableAutoSizing;
    }
}
