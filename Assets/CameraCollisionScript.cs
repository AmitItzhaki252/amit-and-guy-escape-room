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
    void Update()
    {
        //    var a = Input.GetAxis("Horizontal");
        //    Debug.Log(a);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blocking"))
        {
            var playerPosition = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position;


            Debug.Log("Blocking");

            Vector3 newPostion = playerPosition + (playerPosition - other.transform.position).normalized;
            newPostion.y = playerPosition.y;

            gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position = newPostion;
        }
        else if (other.gameObject.CompareTag("Pushable"))
        {
            other.GetComponent<Rigidbody>().AddForce(gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.forward * 3000);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("cotact");
        var contactAmount = collision.contacts;
        var a = collision.GetContact(0);
        Debug.Log(a);

        GameObject other = collision.gameObject;

        Animator[] animations = other.GetComponentsInChildren<Animator>();

        var animation = animations[0].GetComponent<Animator>();
        animation.enabled = true;
        
        animation.Play("Base Layer.WheelAnimation", -1, 0f);
        animation.Play("Base Layer.WheelAnimation");

        Debug.Log(animation);
    }
}
