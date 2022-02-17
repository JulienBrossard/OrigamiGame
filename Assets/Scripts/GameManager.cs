using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject resetButton;

    private void Awake()
    {
        instance = this;
    }

    public void Failed()
    {
        ScreenCapture.CaptureScreenshot ("testImage");

        // Read the data from the file
        Debug.Log(Application.persistentDataPath + "/" + "testImage");
        resetButton.SetActive(true);
        Time.timeScale = 0;
    }
    
}
