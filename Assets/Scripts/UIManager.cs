using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject backController;
    [SerializeField] private GameObject controller;
    public static UIManager instance;
    [SerializeField] private Vector3 startPosition;

    private void Awake()
    {
        instance = this;
    }

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

    public void FollowController(Vector3 position)
    {
        if (Time.timeScale !=0)
        {
            controller.transform.position = new Vector3(Mathf.Clamp(position.x,startPosition.x-150,startPosition.x+150),startPosition.y,startPosition.z);
        }
    }

    public void DePopControllers()
    {
        backController.SetActive(false);
        controller.SetActive(false);
    }
}
