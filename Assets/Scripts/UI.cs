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

    void Start()
    {
        Canvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Resume()
    {
        Canvas.SetActive(false);
    }

    public void ExitApp()
    {
        Debug.Log("Exit app");
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }
}
