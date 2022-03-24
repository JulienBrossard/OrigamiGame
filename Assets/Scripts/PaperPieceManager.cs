using TMPro;
using UnityEngine;

public class PaperPieceManager : MonoBehaviour
{
    public static PaperPieceManager instance;
    [SerializeField] private int pieces;
    [SerializeField] private TextMeshProUGUI piecesText;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (pieces != PlayerPrefs.GetInt("PaperPieces", 0))
        {
            piecesText.text = pieces.ToString();
        }
    }

    public void SavePiece()
    {
        pieces = PlayerPrefs.GetInt("PaperPieces", 0);
    }
}
