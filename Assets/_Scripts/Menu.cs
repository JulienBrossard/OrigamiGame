using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    #region Declarations

    [Tooltip("Le nom de la scène chargée lors du reset")]
    [SerializeField] private string resetSceneName;
    [Tooltip("Le nom de la scène du menu principal")]
    [SerializeField] private string mainMenu;
    [Tooltip("Le son d'un bouton lorqu'il est pressé, à récupérer dans le dossier Sounds")]
    [SerializeField] AudioClip buttonPress;
    
    #endregion
    
    public void Reset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(resetSceneName);
    }

    public void MainMenu()
    {
        AudioManager.instance.PlaySound(buttonPress, 1, 1, 0);
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenu);
    }

    public void Quit()
    {
        AudioManager.instance.PlaySound(buttonPress, 1, 1, 0);
        Application.Quit();
    }

    public void Pause()
    {
        AudioManager.instance.PlaySound(buttonPress, 1, 1, 0);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        AudioManager.instance.PlaySound(buttonPress, 1, 1, 0);
        Time.timeScale = 1;
    }
}
