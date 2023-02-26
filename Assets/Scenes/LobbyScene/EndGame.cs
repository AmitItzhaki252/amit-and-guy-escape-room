using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    private IEnumerator coroutine;
    private bool isDone = false;

    public AudioSource levelCloser;
    public TextMeshProUGUI gameOverText;

    private string[] names = new string[] { "Amit", "Guy", "Omri", "Cow", "Lama", "Sheep", "Scary singer", "Jane", "Jhon", "Dow", "The Ghost" };

    // Start is called before the first frame update
    void Start()
    {
        coroutine = ShowEndGameAndQuit();

        TimeHolder.timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.transform.position.z);

        TimeHolder.Update();

        if (!isDone && gameObject.transform.position.z >= 105f)
        {
            isDone = true;
            TimeHolder.timerOn = false;
            levelCloser.Play(0);

            gameOverText.gameObject.SetActive(true);
            var chosenName = names[Random.Range(0, names.Length)];

            gameOverText.text = gameOverText.text + chosenName + ": " + TimeHolder.timeSTR;

            SaveHigeScore(chosenName);

            StartCoroutine(coroutine);
        }
    }

    IEnumerator ShowEndGameAndQuit()
    {
        while (true)
        {
            //TODO: Show gameover

            Debug.Log($"Game over will be shown for {levelCloser.clip.length} seconds");

            yield return new WaitForSeconds(levelCloser.clip.length);

            Debug.Log($"Game over done");

            StopCoroutine(coroutine);

            Application.Quit();
            EditorApplication.isPlaying = false;

            yield return null;
        }
    }

    void SaveHigeScore(string chosenName)
    {
        var prefScoreName1 = PlayerPrefs.GetString("ScoreName1");
        var prefScore1 = PlayerPrefs.GetInt("Score1", 0);

        var prefScoreName2 = PlayerPrefs.GetString("ScoreName2");
        var prefScore2 = PlayerPrefs.GetInt("Score2", 0);

        var prefScoreName3 = PlayerPrefs.GetString("ScoreName3");
        var prefScore3 = PlayerPrefs.GetInt("Score3", 0);

        var currentScore = TimeHolder.secN;
        var currentName = chosenName;

        if (currentScore <= prefScore3)
        {
            if (currentScore <= prefScore2)
            {
                if (currentScore <= prefScore1)
                {
                    PlayerPrefs.SetString("ScoreName1", currentName);
                    PlayerPrefs.SetInt("Score1", currentScore);

                    currentName = prefScoreName1;
                    currentScore = prefScore1;
                }

                PlayerPrefs.SetString("ScoreName2", currentName);
                PlayerPrefs.SetInt("Score2", currentScore);

                currentName = prefScoreName2;
                currentScore = prefScore2;
            }

            PlayerPrefs.SetString("ScoreName3", currentName);
            PlayerPrefs.SetInt("Score3", currentScore);
        }
    }
}
