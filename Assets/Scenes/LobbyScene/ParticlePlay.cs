using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlay : MonoBehaviour
{ 
    public GameObject particle;
    public int t;

    // Start is called before the first frame update
    void Start()
    {
        t = 2;
    }

    // Update is called once per frame
    void Update()
    {
        t = t - Time.deltaTime;
        if (t <= 0)
            ParticlePlay.SetActive(false);
    }
}
