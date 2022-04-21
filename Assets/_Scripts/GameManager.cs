using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Declarations

    public static GameManager instance;

    [SerializeField] private GameObject deathMenu;
    
    #endregion
    
    private void Awake()
    {
        instance = this;
    }

    public void Failed()
    {
        //ScreenCapture.CaptureScreenshot ("testImage");

        // Read the data from the file
        //Debug.Log(Application.persistentDataPath + "/" + "testImage");
        
        //Active le menu de mort
        deathMenu.SetActive(true);
        Time.timeScale = 0;
        //Sauvegarde le jeu
        ScoreManager.instance.SaveBestScore();
        PaperPieceManager.instance.SavePiece();
        Save();
    }

    void Save()
    {
        PlayerPrefs.Save();
    }
    
}
