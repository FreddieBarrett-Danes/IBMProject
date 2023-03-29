using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour
{
    private Canvas canvas;
    private RectTransform rectTransform;

    [SerializeField] private float referenceWidth = 1920f; // Reference width for scaling
    [SerializeField] private float referenceHeight = 1080f; // Reference height for scaling

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        ScaleUI();
    }

    private void ScaleUI()
    {
        float scaleFactor = 1f;
        float screenRatio = Screen.width / (float)Screen.height;
        float referenceRatio = referenceWidth / referenceHeight;

        if (screenRatio >= referenceRatio)
        {
            scaleFactor = referenceWidth / Screen.width;
        }
        else
        {
            scaleFactor = referenceHeight / Screen.height;
        }

        rectTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }
}