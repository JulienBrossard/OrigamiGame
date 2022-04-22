using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    #region Declarations

    [SerializeField] private string resetSceneName;
    [SerializeField] private string mainMenu;
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
