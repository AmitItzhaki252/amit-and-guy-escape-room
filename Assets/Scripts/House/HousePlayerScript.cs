using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

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

            yield return new WaitForSeconds(levelCloser.clip.length);

            shockEffect.enabled = false;

            StopCoroutine(coroutine);

            GoToNextStage();

            yield return null;
        }
    }

    void GoToNextStage()
    {
        //TODO: GoToNextStage
    }

    public void StartChallenge()
    {
        StartSound();

        TimerOn = true;
        //Score.text = "3rd: " + Time.realtimeSinceStartup; //or coins.SetText(“text”);
    }

    void StopAnimation(GameObject obj)
    {
        var animation = obj.GetComponent<Animator>();
        animation.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        print("Colide " + other);
    }
}