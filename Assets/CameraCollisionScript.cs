using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using System.Threading;
using UnityEditor;

public class CameraCollisionScript : MonoBehaviour
{
    public Vector3 defaultLocation;
    public Vector3 destinationLocation;

    public GameObject objectToDisable = null;

    public PlayerScript playerScript;

    public Volume heartEffect;

    private IEnumerator heartCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        heartCoroutine = RunHeartEffect();

        if (Application.isEditor)
            playerScript = GameObject.Find("XR Origin").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger {other.gameObject.name}");

        if (other.gameObject.CompareTag("Blocking"))
        {
            Debug.Log($"Trigger Blocking ${gameObject} hit ${other}");
            if (heartEffect != null)
            {
                if (!heartEffect.enabled)
                    StartCoroutine(heartCoroutine);
            }
            gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position = defaultLocation;
        }
        else if (other.gameObject.CompareTag("Cheat"))
        {
            Debug.Log($"Trigger Cheat ${gameObject} hit ${other}");

            OrderSolution();
        }
        else if (!(playerScript is null) && other.gameObject.name == "Start btn")
        {
            playerScript.StartChallenge();
        }
        else if (!(playerScript is null) && other.gameObject.name == "Exit btn")
        {
            UnityEditor.EditorApplication.isPlaying = false;
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

            foreach (var animation in animations)
            {
                var animationComponent = animation.GetComponent<Animator>();
                animationComponent.enabled = true;

                animationComponent.Play("Base Layer.WheelAnimation", -1, 0f);
                animationComponent.Play("Base Layer.WheelAnimation");
            }
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