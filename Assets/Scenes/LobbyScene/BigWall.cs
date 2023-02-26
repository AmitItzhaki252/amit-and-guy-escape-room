using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWall : MonoBehaviour
{
    public GameObject wall;
    public AudioSource breakSound;
    public int HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
        //hitSound = GetComponent<AudioSource>();
       // breakSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BigHammer"))
        {
            HP = HP - 1;
            if(HP==0)
            wall.SetActive(false);
            else
            breakSound.Play();
        }
    }
}