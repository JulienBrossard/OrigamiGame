using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Declarations

    public static GameManager instance;

    [Tooltip("Le parent qui regroupe tous les boutons/textes du menu de morts, à récupérer dans l'enfant du canvas")]
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject UIScore;

    [Header("Sound")] [SerializeField] private AudioClip gameOverSound;
    
    #endregion
    
    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
    }

    public void Failed()
    {
        //ScreenCapture.CaptureScreenshot ("testImage");

        // Read the data from the file
        //Debug.Log(Application.persistentDataPath + "/" + "testImage");
        
        //Active le menu de mort
        deathMenu.SetActive(true);
        pause.SetActive(false);
        UIScore.SetActive(false);
        AudioManager.instance.PlaySound(gameOverSound,1,1,0);
        Time.timeScale = 0;
        //Sauvegarde le jeu
        ScoreManager.instance.SaveBestScores();
        PaperPieceManager.instance.SavePiece();
        Save();
    }

    void Save()
    {
        PlayerPrefs.Save();
    }
    
}
