using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseWinButton : MonoBehaviour
{
    public void MoveMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
