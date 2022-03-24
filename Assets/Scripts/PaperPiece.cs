using UnityEngine;

public class PaperPiece : MonoBehaviour, ICollectible
{
    [SerializeField] private int pieces;

    public void Collect()
    {
        pieces = PlayerPrefs.GetInt("PaperPieces", 0);
        PlayerPrefs.SetInt("PaperPieces",pieces++);
        Pooler.instance.DePop("PaperPiece",gameObject);
    }
}
