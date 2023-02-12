using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

    public Volume shockEffect;

    private IEnumerator coroutine;
    private bool hasFinishedStage;


    // Start is called before the first frame update
    void Start()
    {
        StartSound();

        //TODO: This line should run after pressing start escape room button
        //StartChallenge();

        coroutine = RunShockEffectAndLeaveStage();

        hasFinishedStage = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        //TODO: GoToNextStage
    }

    void StartChallenge()
    {
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
