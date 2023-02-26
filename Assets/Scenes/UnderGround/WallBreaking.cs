using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WallBreaking : MonoBehaviour
{
    public GameObject wall;
    public AudioSource breakSound;
    public int HP;
    public string tag;
    public GameObject particle;

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
        if (other.gameObject.CompareTag(tag))
        {
            HP = HP - 1;
            if (HP == 0)
            {
                particle.SetActive(true);
                wall.SetActive(false);
            }
            else
            breakSound.Play();
        }
    }
}