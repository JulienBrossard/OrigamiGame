using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject deathMenu;

    private void Awake()
    {
        instance = this;
    }

    public void Failed()
    {
        ScreenCapture.CaptureScreenshot ("testImage");

        // Read the data from the file
        Debug.Log(Application.persistentDataPath + "/" + "testImage");
        
        deathMenu.SetActive(true);
        Time.timeScale = 0;
        ScoreManager.instance.SaveBestScore();
        PaperPieceManager.instance.SavePiece();
        Save();
    }

    void Save()
    {
        PlayerPrefs.Save();
    }
    
}
