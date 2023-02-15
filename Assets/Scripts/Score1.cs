using UnityEngine;
using System.Collections;
using TMPro;

public class Score1 : MonoBehaviour
{
    public TMP_Text Score;
    void Start()
    {
        Score = GetComponent<TMP_Text>();
    }

    void Update()
    {
        Score.text = "1st: " + "00:40"; //or coins.SetText(“text”);
    }
}