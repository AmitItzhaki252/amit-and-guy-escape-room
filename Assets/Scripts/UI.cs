using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject Canvas;
 //   public TMP_Text Score1;
 //   public TMP_Text Score2;
 //   public TMP_Text Score3;
  //  int time;
    // public GameObject GameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
 //       time = 0;
        Canvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
 //       time++;
 //       Score1.SetText("Score is: " + time);
 //       if (Input.GetKeyDown(KeyCode.P))
 //          {
 //               PauseGame();
 //          }
    }
    public void Resume()
    {
        Canvas.SetActive(false);
        //    PausePanel.SetActive(false);
        //    Time.timeScale = 1f;
    }
    
    private void PauseGame()
    {
        Canvas.SetActive(true);
        //Time.timeScale = 0f;
    }
    public void ExitApp()
    {
        Debug.Log("Exit app");
        Application.Quit();
        EditorApplication.isPlaying = false;

    }
    /**
    public void Resume()
    {
        Canvas.SetActive(false);
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        Canvas.SetActive(true);
        Time.timeScale = 0f;
    }
*/
}
