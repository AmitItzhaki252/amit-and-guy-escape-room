using UnityEngine;
using System.Collections;
using TMPro;

public class ScoreCalculation : MonoBehaviour
{
    public TMP_Text Score1;
    void Start()
    {
        Score1 = GetComponent<TMP_Text>();
    }

    void Update()
    {
        Score1.text = "text"; //or coins.SetText(“text”);
    }
}