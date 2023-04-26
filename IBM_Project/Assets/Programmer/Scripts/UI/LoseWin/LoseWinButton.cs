using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LoseWinButton : MonoBehaviour
{
    public float scalar;
    private RectTransform canvas;
    private GameObject menu;
    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();
        menu = GameObject.Find("Menu");
    }
    public void MoveMainMenu()
    {
        Destroy(menu);
        SceneManager.LoadScene(0);
    }
    private void OnGUI()
    {
        float size = Mathf.Min(canvas.sizeDelta.x, canvas.sizeDelta.y) * (scalar / 100.0f);
        this.transform.localScale = new Vector2(size, size);
    }

}
