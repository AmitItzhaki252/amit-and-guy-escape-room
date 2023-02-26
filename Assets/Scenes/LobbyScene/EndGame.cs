using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.transform.position.z);
        if (gameObject.transform.position.z >= 105f)
        {
            Application.Quit();
            EditorApplication.isPlaying = false;
        }
    }

}
