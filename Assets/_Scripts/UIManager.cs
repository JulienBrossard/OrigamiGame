using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Declarations

    [Tooltip("L'image du fond du controller, à récupérer dans l'enfant du canvas")]
    [SerializeField] private GameObject backController;
    [Tooltip("L'image qui suit le doigt du joueur, à récupérer dans l'enfant du canvas")]
    [SerializeField] private GameObject controller;
    public static UIManager instance;
    private Vector3 startPosition;
    
    #endregion

    private void Awake()
    {
        instance = this;
    }

    //Spawn l'UI du controller
    public void SpawnControllers(Vector3 position)
    {
        if (Time.timeScale!=0)
        {
            backController.SetActive(true);
            controller.SetActive(true);
            backController.transform.position = position;
            controller.transform.position = position;
            startPosition = position;
        }
    }

    //Update la position de l'UI du controller
    public void FollowController(Vector3 position)
    {
        if (Time.timeScale !=0)
        {
            controller.transform.position = new Vector3(Mathf.Clamp(position.x,startPosition.x-300,startPosition.x+300),startPosition.y,startPosition.z);
        }
    }

    //DePop le controller
    public void DePopControllers()
    {
        backController.SetActive(false);
        controller.SetActive(false);
    }
}
