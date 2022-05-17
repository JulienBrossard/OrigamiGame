using TMPro;
using UnityEngine;

public class ScoreManagerMainMenu : MonoBehaviour
{
    [Tooltip("Attention, l'ordre des scores dans le tableau est important !")]
    [SerializeField] private TextMeshProUGUI[] bestScores;

    private void Start()
    {
        for (int i = 0; i < bestScores.Length; i++)
        {
            bestScores[i].text = PlayerPrefs.GetInt("BestScore" + i, 0).ToString();
        }
    }
}
