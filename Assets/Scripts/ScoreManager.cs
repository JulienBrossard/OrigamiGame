using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score;
    [SerializeField] private Transform playerTransform;

    void Update()
    {
        score = (int) Math.Round(playerTransform.position.z,0);
        scoreText.text = "Score : " + score;
    }
}
