using UnityEngine;

public class PaperPiece : MonoBehaviour, ICollectible
{
    #region Declarations

    [SerializeField] private int pieces;
    
    #endregion

    public void Collect()
    {
        pieces = PlayerPrefs.GetInt("PaperPieces", 0);
        PlayerPrefs.SetInt("PaperPieces",++pieces);
        Pooler.instance.DePop("CloudPieces",gameObject);
    }
}
