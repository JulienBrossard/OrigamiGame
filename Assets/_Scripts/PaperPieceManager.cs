using TMPro;
using UnityEngine;

public class PaperPieceManager : MonoBehaviour
{
    #region Declarations

    public static PaperPieceManager instance;
    private int pieces;
    [Tooltip("Le texte qui affiche les pièces du joueur, à récupérer dans l'enfant du canvas")]
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

    public void TutoReward(int reward)
    {
        if (PlayerPrefs.GetInt("FirstTime",0)==0)
        {
            pieces = PlayerPrefs.GetInt("PaperPieces", 0);
            PlayerPrefs.SetInt("PaperPieces",pieces+reward);
            PlayerPrefs.SetInt("FirstTime",1);
        }
    }
}
