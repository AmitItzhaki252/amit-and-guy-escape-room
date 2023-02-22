using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreaking : MonoBehaviour
{
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("C-O-L-L-I-T-I-O-N");
        if (other.gameObject.CompareTag("Hammer"))
        {
            Debug.Log("Hammer");
            //wall.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Not Hammer");
        }
    }
}