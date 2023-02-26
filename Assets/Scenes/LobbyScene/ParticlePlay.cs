using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlay : MonoBehaviour
{ 
    public GameObject particle;
    public float t;

    // Start is called before the first frame update
    void Start()
    {
        t = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        t = t - Time.deltaTime;
        if (t <= 0)
            particle.SetActive(false);
    }
}
