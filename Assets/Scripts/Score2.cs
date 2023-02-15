using UnityEngine;
using System.Collections;
using TMPro;

public class Score2 : MonoBehaviour
{
    public TMP_Text Score;
    void Start()
    {
        Score = GetComponent<TMP_Text>();
    }

    void Update()
    {
        Score.text = "2nd: " + "00:50"; //or coins.SetText(“text”);
    }
}