using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowScript : MonoBehaviour
{
    private Animator animator;
    public AudioSource crowSound;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGazing()
    {
        Debug.Log("Gazing");

        animator.Play("sing", -1, 100000f);
    }

    public void StopGazing()
    {
        Debug.Log("Stopped Gazing");

        animator.Play("Idle", -1, 100000f);
    }

    public void PlaySong()
    {
        Debug.Log("Play song");

        crowSound.Play(0);
    }
}
