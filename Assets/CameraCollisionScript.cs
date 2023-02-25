using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using System.Threading;

public class CameraCollisionScript : MonoBehaviour
{
    public Vector3 defaultLocation;
    public Vector3 destinationLocation;

    public GameObject objectToDisable;

    public Volume heartEffect;

    private IEnumerator heartCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        heartCoroutine = RunHeartEffect();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blocking"))
        {
            Debug.Log($"Trigger Blocking ${gameObject} hit ${other}");

            if (!heartEffect.enabled)
                StartCoroutine(heartCoroutine);

            gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position = defaultLocation;
        }
        else if (other.gameObject.CompareTag("Cheat"))
        {
            Debug.Log($"Trigger Cheat ${gameObject} hit ${other}");

            OrderSolution();
        }
    }

    private void OrderSolution()
    {
        if (!(objectToDisable is null))
            objectToDisable.SetActive(false);

        gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position = destinationLocation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.gameObject.CompareTag("Blocking"))
        {
            Debug.Log($"Collision Blocking ${gameObject} hit ${other}");

            StartCoroutine(heartCoroutine);

            gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position = defaultLocation;
        }
        else if (other.gameObject.CompareTag("Pushable"))
        {
            Debug.Log($"Collision Pushable ${gameObject} hit ${other}");

            Animator[] animations = other.GetComponentsInChildren<Animator>();

            var animation = animations[0].GetComponent<Animator>();
            animation.enabled = true;

            animation.Play("Base Layer.WheelAnimation", -1, 0f);
            animation.Play("Base Layer.WheelAnimation");
        }
        else if (other.gameObject.CompareTag("Cheat"))
        {
            Debug.Log($"Collision Cheat ${gameObject} hit ${other}");

            OrderSolution();
        }
    }

    IEnumerator RunHeartEffect()
    {
        while (true)
        {
            if (!heartEffect.enabled)
                heartEffect.enabled = true;

            Debug.Log($"Start running heart effect for {3} secinds");

            yield return new WaitForSeconds(3.0f);

            if (heartEffect.enabled)
                heartEffect.enabled = false;

            Debug.Log($"Done running heart effect");

            StopCoroutine(heartCoroutine);

            yield return null;
        }
    }
}