using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using System.Threading;
using System;
using UnityEngine.SceneManagement;

public class TimeHolder : MonoBehaviour
{
    public static int secN;
    public static int minN;
    public static string sec;
    public static string min;
    public static bool timerOn;
    public static float time;
    public static string timeSTR = "00:00";

    public static void Setup()
    {
        timerOn = false;

        time = 0f;
        secN = 0;
        minN = 0;
    }

    public static void Update()
    {
        if (!timerOn)
            return;

        time += Time.deltaTime;
        secN = (int)Math.Round(time);
        minN = secN / 60;
        if (secN % 60 > 9)
            sec = (secN % 60).ToString();
        else
            sec = "0" + (secN % 60).ToString();
        if (minN > 9)
            min = minN.ToString();
        else
            min = "0" + minN.ToString();
        timeSTR = min + ":" + sec;
    }
}
