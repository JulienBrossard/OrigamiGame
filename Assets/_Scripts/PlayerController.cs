using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Declarations

    public static PlayerController instance;
    
    [Header("Rigidbody")]
    [Tooltip("Le rigidbody du joueur, à récupérer sur le joueur")]
    [SerializeField] private Rigidbody rb;
    [Header("Max Horizontal Position")]
    [SerializeField] private float maxHorizontalPosition;
    [Header("Sensibility")]
    [Tooltip("La sensibilité du controller du joueur, de base à 65")]
    [SerializeField] private float sensibility;
    [SerializeField] public Vector3 externalForce;

    #endregion
    
   
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (LevelManager.instance != null)
        {
            maxHorizontalPosition = LevelManager.instance.levelWidth;
        }
        sensibility = PlayerPrefs.GetInt("Sensibility", 45);
    }

    public void Controller(float difference)
    {
        if (Time.timeScale!=0)
        {
            //Cacule de la vitesse
            rb.velocity = new Vector3(-difference * sensibility, rb.velocity.y, rb.velocity.z) - externalForce;
            // Clamp de la position sur le jeu
            transform.position = new Vector3(Mathf.Clamp(transform.position.x,-maxHorizontalPosition,maxHorizontalPosition), transform.position.y, transform.position.z);
        }
    }

    public void ExternalForce()
    {
        rb.velocity = new Vector3(-externalForce.x,rb.velocity.y+externalForce.y,rb.velocity.z+externalForce.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-maxHorizontalPosition,maxHorizontalPosition), transform.position.y, transform.position.z);
    }
}
