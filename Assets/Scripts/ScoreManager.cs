using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score;
    [SerializeField] private int bestScore;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject newRecordText;

    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        score = (int) Math.Round(playerTransform.position.z,0);
        scoreText.text = "Score : " + score;
    }

    public void SaveBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score>bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore",bestScore);
            PlayerPrefs.Save();
            newRecordText.SetActive(true);
        }
    }
}
