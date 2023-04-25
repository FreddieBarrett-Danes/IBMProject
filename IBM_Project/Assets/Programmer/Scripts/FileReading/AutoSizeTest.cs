using TMPro;
using UnityEngine;

public class AutoSizeTest : MonoBehaviour
{
    public float size;
    public bool isAuto;

    // Update is called once per frame
    void Update()
    {
        size = GetComponent<TextMeshProUGUI>().fontSize;
        isAuto = GetComponent<TextMeshProUGUI>().enableAutoSizing;
    }
}
