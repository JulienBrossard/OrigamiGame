using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Reset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Prototype");
    }
}
