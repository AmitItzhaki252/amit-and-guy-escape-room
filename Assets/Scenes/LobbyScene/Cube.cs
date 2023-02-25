using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject ObjectToCopyPosition;
    public GameObject ObjectToPull;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = ObjectToCopyPosition.transform.position.x;
        float y = ObjectToCopyPosition.transform.position.y;
        float z = ObjectToCopyPosition.transform.position.z;
        ObjectToPull.transform.position = new Vector3(x, y, z);
    }
}
