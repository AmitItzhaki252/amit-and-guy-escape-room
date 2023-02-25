using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.SceneManagement;

public class HousePlayerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject floor;
    public GameObject target;
    public GameObject crow;
    public AudioSource ambience;
    public AudioSource levelOpener;
    public AudioSource levelCloser;
    public TMP_Text timetxt;
    public Volume shockEffect;
    private IEnumerator coroutine;
    private bool hasFinishedStage;

    public GameObject cheat;

    public bool TimerOn;
    public int time { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        coroutine = RunShockEffectAndLeaveStage();
        timetxt = GetComponent<TMP_Text>();
        hasFinishedStage = false;
        TimerOn = true;
        time = 0;
        
        if (Application.isEditor)
            cheat.SetActive(true);

        StartChallenge();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn && !(timetxt is null))
        {
            timetxt.text = time.ToString();
        }
        if (!hasFinishedStage && HasReachedTarget())
        {
            hasFinishedStage = true;

            FinishStage();

            return;
        }
    }

    void StartSound()
    {

        levelOpener.Play(0);
        ambience.PlayDelayed(levelOpener.clip.length);
    }

    bool HasReachedTarget()
    {
        float distance = Vector3.Distance(player.transform.position, target.transform.position);

        return distance < 4.8f;
    }

    void FinishStage()
    {
        TimerOn = false;

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
        SceneManager.LoadScene("TstScene");
    }

    public void StartChallenge()
    {
        Debug.Log($"Challenge started");

        StartSound();

        TimerOn = true;
        //Score.text = "3rd: " + Time.realtimeSinceStartup; //or coins.SetText(“text”);
    }

    void StopAnimation(GameObject obj)
    {
        var animation = obj.GetComponent<Animator>();
        animation.enabled = false;
    }
}