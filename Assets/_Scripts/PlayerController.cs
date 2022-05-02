using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Declarations

    public static PlayerController instance;
    
    [Header("Rigidbody")]
    [Tooltip("Le rigidbody du joueur, à récupérer sur le joueur")]
    [SerializeField] private Rigidbody rb;
    private float maxHorizontalPosition;
    [Header("Sensibility")]
    [Tooltip("La sensibilité du controller du joueur, de base à 65")]
    [SerializeField] private float sensibility;
    [HideInInspector] public Vector3 externalForce;
    
    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        maxHorizontalPosition = LevelManager.instance.levelWidth;
        sensibility = PlayerPrefs.GetInt("Sensibility", 45);
    }

    private void FixedUpdate()
    {
        Controller();
    }

    public void Controller()
    {
        if (Time.timeScale!=0)
        {
            //Cacule de la vitesse
            rb.velocity = new Vector3((InputManager.instance.startTouchPosition - InputManager.instance.difference) * sensibility, rb.velocity.y, rb.velocity.z) - externalForce;
            // Clamp de la position sur le jeu
            transform.position = new Vector3(Mathf.Clamp(transform.position.x,-maxHorizontalPosition,maxHorizontalPosition), transform.position.y, transform.position.z);
        }
    }
}
