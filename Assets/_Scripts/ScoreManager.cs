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
    [Header("Texts")] 
    [SerializeField] private TextMeshProUGUI scoreTextGameOver;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    

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

    public void SaveBestScores()
    {
        for (int i = 0; i < 4; i++)
        {
            if (score>PlayerPrefs.GetInt("BestScore"+i,0))
            {
                for (int j = 0; j < 3-i; j++)
                {
                    PlayerPrefs.SetInt("BestScore"+(3-j),PlayerPrefs.GetInt("BestScore"+(3-j-1),0));
                }
                PlayerPrefs.SetInt("BestScore" + i, score);
                break;
            }
        }
        scoreTextGameOver.text = score.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore0", 0).ToString();
    }
}
