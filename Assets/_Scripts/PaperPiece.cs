using UnityEngine;

public class PaperPiece : MonoBehaviour, ICollectible
{
    #region Declarations

    [Tooltip("Le nombre de pièces du joueur, rien à changer ici, c'est juste un Debug")]
    [SerializeField] private int pieces;
    
    #endregion

    public void Collect()
    {
        //Collecte les pièces
        pieces = PlayerPrefs.GetInt("PaperPieces", 0);
        PlayerPrefs.SetInt("PaperPieces",++pieces);
        gameObject.SetActive(false);
    }
}
