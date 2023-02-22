using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject sheep;
    public GameObject cow;
    public GameObject showingFlowers;
    public GameObject target;
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
        StartSound();

        //TODO: This line should run after pressing start escape room button
        //StartChallenge();

        coroutine = RunShockEffectAndLeaveStage();
        timetxt = GetComponent<TMP_Text>();
        hasFinishedStage = false;
        TimerOn = false;
        player.transform.position = new Vector3(453, 15, 340);
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            Debug.Log("calc");

            //    timetxt.text = time.ToString();
        }
        if (!hasFinishedStage && HasReachedTarget())
        {
            hasFinishedStage = true;

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

            yield return new WaitForSeconds(levelCloser.clip.length);

            shockEffect.enabled = false;

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

        TimerOn = true;
        Debug.Log("Started");
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

    void OnTriggerEnter(Collider other)
    {
        print("Colide " + other);
    }

}
