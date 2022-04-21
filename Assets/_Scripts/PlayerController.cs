using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Declarations

    public static PlayerController instance;
    
    [Header("Rigidbody")]
    [SerializeField] private Rigidbody rb;
    private float maxHorizontalPosition;
    [Header("Sensibility")]
    [SerializeField] private float sensibility;
    [HideInInspector] public Vector3 externalForce;
    
    #endregion

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
            //Cacule de la vitesse
            rb.velocity = new Vector3((startPositionX - positionX) * sensibility, rb.velocity.y, rb.velocity.z) - externalForce;
            // Clamp de la position sur le jeu
            transform.position = new Vector3(Mathf.Clamp(transform.position.x,-maxHorizontalPosition,maxHorizontalPosition), transform.position.y, transform.position.z);
        }
    }
}
