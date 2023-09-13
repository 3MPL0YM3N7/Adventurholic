using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 5;
    bool touchedHazard = false;

    // Canvas text
    [SerializeField] TextMeshProUGUI livesTextfield;
    [SerializeField] TextMeshProUGUI scoreTextfield;
    int scoreAmount = 0;
    // used in GameSession --> increaseLives()
    int scoreCompareValue = 0;

    public void increaseScoreAmount(int coinScoreAmount)
    {
        scoreAmount += coinScoreAmount;
        scoreTextfield.text = scoreAmount.ToString();

        // used in GameSession --> increaseLives()
        scoreCompareValue += coinScoreAmount;
    }

    public void increaseLives()
    {
        // 1000 is the score that increases the player lives by 1
        if (scoreCompareValue >= 1000)
        {
            scoreCompareValue -= 1000;
            ++playerLives;
            livesTextfield.text = playerLives.ToString();
        }
    }

    IEnumerator reloadLevel()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        touchedHazard = false;
    }

    IEnumerator resetGameSession()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    // called in PlayerDeath
    public void managePlayerLives()
    {
        if (playerLives > 0 && !touchedHazard)
        {
            touchedHazard = true;
            // death sound effect
            FindObjectOfType<PlayerDeath>().playDeathSFX();
            --playerLives;
            StartCoroutine(reloadLevel());
        }
        else if (playerLives <= 0 && !touchedHazard)
        {
            touchedHazard = true;
            // death sound effect
            FindObjectOfType<PlayerDeath>().playDeathSFX();
            StartCoroutine(resetGameSession());
            FindObjectOfType<ScenePersist>().destroyOutdatetScene();
        }
        livesTextfield.text = playerLives.ToString();
    }

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesTextfield.text = playerLives.ToString();
        scoreTextfield.text = scoreAmount.ToString();
    }
}
