using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireMove : MonoBehaviour
{
    private float timer;
    public string tag;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= 10)
            transform.position = new Vector3(6f - timer, -3f, -3f + (timer * 10)); //End pos is (-4,-3,120)
        else
            transform.position = new Vector3(-4f + Convert.ToSingle(Math.Sin(Convert.ToDouble(timer))) * 5, -3f, 120f);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            if (timer > 10)
                Application.Quit();
        }
    }
}
