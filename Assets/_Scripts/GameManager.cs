using System.Collections;
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

    [Header("Dead Particle")] [SerializeField] private GameObject deadParticle;

    [Header("Player")] [SerializeField] private GameObject[] origami;
    [SerializeField] private Animator playerAnimator;
    
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
        StartCoroutine(DeathMenu());
        pause.SetActive(false);
        UIScore.SetActive(false);
        AudioManager.instance.PlaySound(gameOverSound,1,1,0);
        deadParticle.SetActive(true);
        playerAnimator.enabled = false;
        AnimationManager.instance.DoMove(deathMenu.transform,-Screen.width / 2, 1);
        PlayerController.instance.enabled = false;
        for (int i = 0; i < origami.Length; i++)
        {
            origami[i].SetActive(false);
        }
        //Sauvegarde le jeu
        ScoreManager.instance.SaveBestScores();
        PaperPieceManager.instance.SavePiece();
        Save();
    }

    public IEnumerator DeathMenu()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        deathMenu.SetActive(true);
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }
    
}
