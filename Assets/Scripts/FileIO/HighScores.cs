using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScores : MonoBehaviour
{
    public int[] scores = new int[10];

    string currentDirectory;

    public string scoreFileName = "highscores.txt";

    private void Start()
    {
        currentDirectory = Application.dataPath;
        Debug.Log("our current directory is: " + currentDirectory);
        
        LoadScoresFromFile();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadScoresFromFile();
            Debug.Log("Contents of " + scoreFileName + ":");
            foreach (int score in scores)
            {
                Debug.Log(score);
            }
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            SaveScoreToFile();
            Debug.Log("Contents of " + scoreFileName + ":");
            foreach (int score in scores)
            {
                Debug.Log(score);
            }
        }
    }

    public void LoadScoresFromFile()
    {
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFileName);
        if (fileExists == true)
        {
            Debug.Log("Found high score file " + scoreFileName);
        }
        else
        {
            Debug.Log("The file " + scoreFileName +
            " does not exist. No scores will be loaded.", this);
            return;
        }
        scores = new int[scores.Length];
        StreamReader fileReader = new StreamReader(currentDirectory +
        "\\" + scoreFileName);
        int scoreCount = 0;

        while (fileReader.Peek() != 0 && scoreCount < scores.Length)
        {
            string fileLine = fileReader.ReadLine();
            int readScore = -1;
            bool didParse = int.TryParse(fileLine, out readScore);
            if (didParse)
            {
                scores[scoreCount] = readScore;
            }
            else
            {
                Debug.Log("Invalid line in scores file at " + scoreCount +
                ", using default value.", this);
                scores[scoreCount] = 0;
            }
            scoreCount++;
        }
        fileReader.Close();
        Debug.Log("High scores read from " + scoreFileName);
    }
    public void SaveScoreToFile()
    {
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFileName);
        for (int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }
        fileWriter.Close();
    }

    public void AddScore(int newScore)
    {
        int desireIndex = -1;
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] < newScore || scores[i] == 0)
            {
                desireIndex = i;
                break;
            }
        }

        if(desireIndex < 0)
        {
            Debug.Log("score of " + newScore);
            return;
        }
        for (int i = scores.Length - 1; i > desireIndex; i--)
        {
            scores[i] = scores[i - 1];
        }

        scores[desireIndex] = newScore;
    }

}
