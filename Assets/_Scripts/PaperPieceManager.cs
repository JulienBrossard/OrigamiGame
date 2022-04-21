using TMPro;
using UnityEngine;

public class PaperPieceManager : MonoBehaviour
{
    #region Declarations

    public static PaperPieceManager instance;
    private int pieces;
    [SerializeField] private TextMeshProUGUI piecesText;
    
    #endregion
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Récupère la dernière sauvegarde des pièces
        pieces = PlayerPrefs.GetInt("PaperPieces", 0);
        piecesText.text = pieces.ToString();
    }

    private void Update()
    {
        if (pieces != PlayerPrefs.GetInt("PaperPieces", 0))
        {
            pieces = PlayerPrefs.GetInt("PaperPieces", 0);
            piecesText.text = pieces.ToString();
        }
    }

    public void SavePiece()
    {
        pieces = PlayerPrefs.GetInt("PaperPieces", 0);
    }
}
