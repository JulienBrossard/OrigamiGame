using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    #region Declarations
    
    [Header("Score Text")]
    [Tooltip("Le score affiché dans le canvas, à récupérer dans l'enfant du canvas")]
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;
    private int bestScore;
    [Header("Player Transform")]
    [Tooltip("Le transform du joueur, à récupérer dans le joueur")]
    [SerializeField] private Transform playerTransform;
    [Header("New Record")]
    [Tooltip("Le game objet du nouveau record, à récupérer dans l'enfant du canvas")]
    [SerializeField] private GameObject newRecord;
    [Tooltip("Le texte du nouveau record, à récupérer dans l'enfant du canvas")]
    [SerializeField] private TextMeshProUGUI newRecordText;

    public static ScoreManager instance;
    
    #endregion

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
            newRecord.SetActive(true);
            newRecordText.text = "Best Score" + bestScore;
        }
    }
}
