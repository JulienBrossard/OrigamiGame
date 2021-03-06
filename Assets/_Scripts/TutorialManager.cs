using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] public Transform currentCheckpoint;
    [SerializeField] public GameObject tuto;
    [SerializeField] private Transform player;
    public static TutorialManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    //Reccomencer si le joueur loupe le tuto
    public void Retry()
    {
        player.position = currentCheckpoint.position;
        NextCheckpoint(currentCheckpoint);
        tuto.SetActive(true);
        PlayerMovement.instance.ChangeOrigami();
        Time.timeScale = 0;
    }
    
    //Activer l'objet
    public void EnableObject(GameObject go)
    {
        tuto = go;
        tuto.SetActive(!tuto.activeSelf);
    }

    //Update le checkpoint
    public void NextCheckpoint(Transform nextCheckpoint)
    {
        currentCheckpoint = nextCheckpoint;
        // Pool le tuto
        Time.timeScale = 0;
    }
}
