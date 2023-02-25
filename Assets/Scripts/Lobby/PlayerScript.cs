using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using System.Threading;
using System;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject sheep;
    public GameObject cow;
    public GameObject showingFlowers;
    public GameObject target;
    public GameObject canvas;
    public AudioSource ambience;
    public AudioSource levelOpener;
    public AudioSource levelCloser;
    public TMP_Text timetxt;
    public Volume shockEffect;
    private IEnumerator coroutine;
    private bool hasFinishedStage;
    private string timeString;
    public int secN;
    public int minN;
    public string sec;
    public string min;

    public GameObject cheat;

    public bool TimerOn;
    public float time { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        StartSound();

        //TODO: This line should run after pressing start escape room button
        StartChallenge();

        coroutine = RunShockEffectAndLeaveStage();
        hasFinishedStage = false;
        TimerOn = false;
        player.transform.position = new Vector3(453, 15, 335);
        time = 0;
        secN = 0;
        minN = 0;
        timeString = "00:00";

        if (Application.isEditor)
            cheat.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canvas.activeSelf) //when we start, update time.  // TODO: activate/de-activate when we play
        {
            time += Time.deltaTime;
            secN = (int)Math.Round(time);
            minN = secN / 60;
            if (secN % 60 > 9)
                sec = (secN % 60).ToString();
            else
                sec = "0" + (secN % 60).ToString();
            if (minN > 9)
                min = minN.ToString();
            else
                min = "0" + minN.ToString();

            //timetxt.text = min + ":" + sec; //change the text to our time
            //Debug.Log("calc");

            timetxt.text = time.ToString();
        }
        if (!hasFinishedStage && HasReachedTarget())
        {
            hasFinishedStage = true;

            Debug.Log("Stage was finished, runnig finish operations");

            FinishStage();

            return;
        }

        UpdateDirectionToTarget();
    }

    void StartSound()
    {
        levelOpener.Play(0);
        ambience.PlayDelayed(levelOpener.clip.length);
    }

    bool HasReachedTarget()
    {
        float distance = Vector3.Distance(player.transform.position, target.transform.position);

        return distance < 5f;
    }

    void FinishStage()
    {
        StartCoroutine(coroutine);

        ambience.Stop();
        levelCloser.Play(0);
    }

    IEnumerator RunShockEffectAndLeaveStage()
    {
        while (true)
        {
            shockEffect.enabled = true;

            Debug.Log($"Shock effect will run for {levelCloser.clip.length} seconds");

            yield return new WaitForSeconds(levelCloser.clip.length);

            shockEffect.enabled = false;

            Debug.Log($"Shock effect done");

            StopCoroutine(coroutine);

            GoToNextStage();

            yield return null;
        }
    }

    void GoToNextStage()
    {
        SceneManager.LoadScene("House");
    }

    public void StartChallenge()
    {

        Debug.Log("Starting Challenge");

        canvas.SetActive(false);
        //Score.text = "3rd: " + Time.realtimeSinceStartup; //or coins.SetText(“text”);
        showingFlowers.SetActive(true);

        StopAnimation(cow);
        StopAnimation(sheep);

        UpdateDirectionToTarget();
    }

    void StopAnimation(GameObject obj)
    {
        var animation = obj.GetComponent<Animator>();
        animation.enabled = false;
    }

    void UpdateDirectionToTarget()
    {

        var playerLocation = player.transform.position;
        var targetLocation = target.transform.position;

        var directionX = (1.2f * targetLocation.x + playerLocation.x) / 2.2f;
        var directionZ = (1.2f * targetLocation.z + playerLocation.z) / 2.2f;

        var y = 22f;
        sheep.transform.position = new Vector3(directionX + 0.75f, y, directionZ + 0.75f);
        cow.transform.position = new Vector3(directionX - 0.75f, y, directionZ - 0.75f);

        sheep.transform.LookAt(player.transform);
        cow.transform.LookAt(player.transform);
    }
}