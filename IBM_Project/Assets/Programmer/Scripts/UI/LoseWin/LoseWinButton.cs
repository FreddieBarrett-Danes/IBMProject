using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LoseWinButton : MonoBehaviour
{
    private float scalar = 0.2f;
    private RectTransform canvas;
    private GameObject menu;
    private ScoreSystem scoreSystem;
    private TextMeshProUGUI scoreText;
    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();
        menu = GameObject.Find("Menu");
        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.SetText("Final Score: " + scoreSystem.score.ToString());
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
