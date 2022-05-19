using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    #region Declarations

    [Tooltip("Le nom de la scène chargée lors du reset")]
    [SerializeField] public string resetSceneName;
    [Tooltip("Le nom de la scène du menu principal")]
    [SerializeField] private string mainMenu;
    [Tooltip("Le son d'un bouton lorqu'il est pressé, à récupérer dans le dossier Sounds")]
    [SerializeField] AudioClip buttonPress;
    [Tooltip("Le parent regroupant tous les boutons du menu pause")]
    [SerializeField] private Transform pauseMenu;

    public static Menu instance;

    #endregion

    private void Awake()
    {
        instance = this;
    }

    public void Reset()
    {
        if (ChooseLevel.instance != null)
        {
            ChooseLevel.instance.SaveChooseLevel();
        }
        PlayerPrefs.Save();
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
        pauseMenu.gameObject.SetActive(true);
        AnimationManager.instance.DoMove(pauseMenu, -Screen.width / 2, 1);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        AudioManager.instance.PlaySound(buttonPress, 1, 1, 0);
        AnimationManager.instance.DoMove(pauseMenu, Screen.width / 2,1);
        Time.timeScale = 1;
    }
}
