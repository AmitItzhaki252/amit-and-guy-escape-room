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
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    public TextMeshProUGUI score3;
    public AudioSource ambience;
    public AudioSource levelOpener;
    public AudioSource levelCloser;
    public Volume shockEffect;
    private IEnumerator coroutine;
    private bool hasFinishedStage;

    private string[] names = new string[] { "Amit", "Guy", "Omri", "Cow", "Lama", "Sheep", "Scary singer", "Jane", "Jhon", "Dow", "The Ghost" };

    public GameObject cheat;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = new Vector3(456.339996f, 15.4500008f, 342.73999f);

        StartSound();

        coroutine = RunShockEffectAndLeaveStage();
        hasFinishedStage = false;

        TimeHolder.Setup();

        ShowHighScores();

        if (Application.isEditor && !(cheat is null))
            cheat.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        TimeHolder.Update();

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
        TimeHolder.timerOn = false;

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
        
        showingFlowers.SetActive(true);

        StopAnimation(cow);
        StopAnimation(sheep);

        UpdateDirectionToTarget();

        TimeHolder.timerOn = true;
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

    void ShowHighScores()
    {
        var prefScoreName1 = PlayerPrefs.GetString("ScoreName1", "It could be you 1!");
        var prefScore1 = PlayerPrefs.GetInt("Score1", 0);

        score1.text = prefScoreName1 + ": " + prefScore1;
        Debug.Log(score1.text);

        var prefScoreName2 = PlayerPrefs.GetString("ScoreName2", "It could be you 2!");
        var prefScore2 = PlayerPrefs.GetInt("Score2", 0);
        
        score2.text = prefScoreName2 + ": " + prefScore2;

        var prefScoreName3 = PlayerPrefs.GetString("ScoreName3", "It could be you 3!");
        var prefScore3 = PlayerPrefs.GetInt("Score3", 0);
        
        score3.text = prefScoreName3 + ": " + prefScore3;
    }
}