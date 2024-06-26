using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public HighScores highScores;

    public TargetHealth[] targets;
    public GameObject player;
    public Camera worldCamera;

    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    
    public TextMeshProUGUI ammoText;

    public GameObject highScorePanel;
    public TextMeshProUGUI highScoresText;

    public Button newGameButton;
    public Button highScoresButton;
    public Button backButton;

    public float startTimerAmount = 3;
    private float startTimer;

    public float targetActivateTimerAmount = 1;
    private float targetActivateTimer;

    public float gameTimerAmount = 60;
    private float gameTimer;

    private int score = 0;

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    private GameState gameState;

    public GameState State { get {return gameState;} }

    private void Awake()
    {
        gameState = GameState.GameOver;
    }

    private void Start()
    {
        player.SetActive(false);
        worldCamera.gameObject.SetActive(true);

        for(int i = 0; i < targets.Length; i++)
        {
            targets[i].GameManager = this;
            targets[i].gameObject.SetActive(false);
        }
        startTimer = startTimerAmount;
        messageText.text = "";
        timerText.text = "";
        scoreText.text = "";
        ammoText.text = "";

        OnMenu();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch(gameState) 
        { 
            case GameState.Start:
                GameStateStart(); break;
            case GameState.Playing:
                GameStatePlaying(); break;
            case GameState.GameOver: 
                GameStateGameOver(); break;
                
        }
    }

    private void GameStateStart() 
    { 
        startTimer -= Time.deltaTime;
        messageText.text = "Get Ready in:" +(int)(startTimer + 1) ; ;
        if(startTimer <= 0)
        {
            messageText.text = "";
            gameState = GameState.Playing;
            gameTimer = gameTimerAmount;
            startTimer = startTimerAmount;
            score = 0;

            Cursor.lockState = CursorLockMode.Locked;

            highScorePanel.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            newGameButton.gameObject.SetActive(false);
            highScoresButton.gameObject.SetActive(false);

            worldCamera.gameObject.SetActive(false);
            player.SetActive(true);
            AddScore(0);
        }
    }

    private void GameStatePlaying() 
    { 
        gameTimer -= Time.deltaTime;
        int secconds = Mathf.RoundToInt(gameTimer);
        timerText.text = string.Format("Time: {0:D2}:{1:D2}",(secconds/60),(secconds%60));
        if (gameTimer <= 0)
        {
            Debug.Log("Game Over: " + score);
            messageText.text = "Game Over! Score:" + score;
            gameState = GameState.GameOver;
            player.SetActive(false);
            worldCamera.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].gameObject.SetActive(false);
            }
            highScores.AddScore(score);
            highScores.SaveScoreToFile();
            OnMenu();
        }

        targetActivateTimer -= Time.deltaTime;
        if (targetActivateTimer < 0)
        {
            ActivateRandomTarget();
            targetActivateTimer = targetActivateTimerAmount;
        }
    
    }

    private void GameStateGameOver() 
    {
        if (Input.GetKey(KeyCode.Return))
        {
            

            gameState = GameState.Start;
            scoreText.text = "";
            timerText.text = "";
            ammoText.text = "";
        }
        player.SetActive(false);
    }

    private void ActivateRandomTarget()
    {
        int randomIndex = UnityEngine.Random.Range(0, targets.Length);
        targets[randomIndex].gameObject.SetActive(true);
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void OnNewGame()
    {
        gameState = GameState.Start;
    }

    public void OnHighScores()
    {
        messageText.text = "";

        highScoresButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        highScorePanel.gameObject.SetActive(true);

        string text = "";
        for (int i = 0; i < highScores.scores.Length; i++)
        {
            text += highScores.scores[i] + "\n";
        }
        highScoresText.text = text;
    }

    public void OnMenu()
    {
        highScorePanel.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(true);
        highScoresButton.gameObject.SetActive(true);
        messageText.text = "";
    }
}
