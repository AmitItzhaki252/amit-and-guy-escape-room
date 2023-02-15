using UnityEngine;
using System.Collections;
using TMPro;



public class Score3 : MonoBehaviour
{
    public TMP_Text Score;
    public float time;
    void Start()
    {
        time = 0;
        Score = GetComponent<TMP_Text>();
    }

    void Update()
    {
        
        Score.text = "3rd: " + Time.realtimeSinceStartup; //or coins.SetText(“text”);
    }
}