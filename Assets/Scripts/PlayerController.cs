using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxHorizontalPosition;
    [SerializeField] private float sensibility;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        maxHorizontalPosition = LevelManager.instance.maxHorizontalPosition;
    }

    public void Controller(float positionX, float startPositionX)
    {
        if (Time.timeScale!=0)
        {
            //Debug.Log((startPositionX - positionX) * sensibility);
            rb.velocity = new Vector3((startPositionX - positionX) * sensibility, rb.velocity.y, rb.velocity.z);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x,-maxHorizontalPosition,maxHorizontalPosition), transform.position.y, transform.position.z);
            //transform.position = new Vector3(Mathf.Clamp(transform.position.x + positionX,-maxHorizontalPosition,maxHorizontalPosition), transform.position.y, transform.position.z);
        }
    }
}
