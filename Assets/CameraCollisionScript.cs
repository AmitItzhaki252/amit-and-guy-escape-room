using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blocking"))
        {
            var playerPosition = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position;

            Vector3 newPostion = playerPosition + (playerPosition - other.transform.position).normalized;
            newPostion.y = playerPosition.y;

            gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position = newPostion;
        }
    }
}
