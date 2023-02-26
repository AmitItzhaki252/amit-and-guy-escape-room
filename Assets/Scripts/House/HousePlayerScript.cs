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
    public Volume darkEffect;
    private IEnumerator coroutine;
    private bool hasFinishedStage;

    public GameObject cheat;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = new Vector3(24.5799999f, 6f, -3.29999995f);

        coroutine = RunDarkEffectAndLeaveStage();
        
        if (Application.isEditor)
            cheat.SetActive(true);

        StartChallenge();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFinishedStage && HasReachedTarget())
        {
            TimeHolder.timerOn = false;
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
        StartCoroutine(coroutine);

        ambience.Stop();
        levelCloser.Play(0);
    }

    IEnumerator RunDarkEffectAndLeaveStage()
    {
        while (true)
        {
            darkEffect.enabled = true;

            Debug.Log($"Shock effect will run for {levelCloser.clip.length} seconds");

            yield return new WaitForSeconds(levelCloser.clip.length);

            darkEffect.enabled = false;

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

        TimeHolder.timerOn = true;

        StartSound();
    }
}