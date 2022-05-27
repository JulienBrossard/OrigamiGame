using UnityEngine;

public class PaperPiece : MonoBehaviour, ICollectible
{
    #region Declarations

    [Tooltip("Le nombre de pièces du joueur, rien à changer ici, c'est juste un Debug")]
    [SerializeField] private int pieces;
    [Header("Sounds")]
    [SerializeField] private AudioClip[] sounds;
    
    #endregion

    public void Collect()
    {
        //Collecte les pièces
        pieces = PlayerPrefs.GetInt("PaperPieces", 0);
        PlayerPrefs.SetInt("PaperPieces",++pieces);
        AudioManager.instance.PlaySound(sounds[Random.Range(0,sounds.Length)],0.1f,1,0);
        gameObject.SetActive(false);
    }
}
