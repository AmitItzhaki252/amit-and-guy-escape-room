using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using System.Threading;
using System;
using UnityEngine.SceneManagement;

public class UI3rd : MonoBehaviour
{
    public Transform target;

    public GameObject cheat;

    void Start()
    {
        if (Application.isEditor && !(cheat is null))
            cheat.SetActive(true);
    }

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(target);
    }
}
