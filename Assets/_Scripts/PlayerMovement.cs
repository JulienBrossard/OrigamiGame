using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    #region Declarations
    
    [Tooltip("Le rigidbody du joueur, à récupérer sur le joueur")]
    [SerializeField] private Rigidbody rb;

    [Tooltip("Le fov lors du décollage de l'avion, de base à 90")]
    [SerializeField] private float fov;

    [SerializeField] private float baseFov = 60;

    Origami origamiChangement;

    #region Horizontal Speeds

    [Header("Horizontal Speeds", order = 1)]
    [Tooltip("Le boost de vitesse du bateau sur un nuage classique, de base à 0.3")]
    public float boostSpeed;
    [Tooltip("Le boost de vitesse du bateau sur un nuage pluvieux, de base à ?")]
    public float boostRainSpeed;
    [Tooltip("La réduction de vitesse lors du décollage de l'avion, de base à 0.3")]
    [SerializeField] private float reduceSpeed;
    [HideInInspector] public float speed;
    
    #endregion
    
    
    #region Vertical Speeds

    [Header("Vertcial Speed", order =1)]
    [Tooltip("La vitesse du décollage de l'avion, de base à -7")]
    [SerializeField] private float ascendSpeed;
    [SerializeField] public float fallSpeed;
    
    #endregion
    
    
    #region Cloud Booleans
    [HideInInspector] public bool isExitCloud;
    [HideInInspector] public bool isCloud;
    
    #endregion
    

    #region Collider

    [Header("Colliders", order =1)]
    [Tooltip("Le box collider du joueur, à récupérer sur le joueur")]
    [SerializeField] private BoxCollider boxCollider;

    #endregion

    #endregion

    float origamiSpeed
    {
        get => speed;
        set
        {
            speed = value;
            AnimationManager.instance.OrigamiTransition();
        }
    }

    private void Start()
    {
        #region Initialize Speeds

        speed = PlayerManager.origami[PlayerManager.state].speed;
        fallSpeed = PlayerManager.origami[PlayerManager.state].fallSpeed;

        #endregion
    }

    void FixedUpdate()
    {
        //Vitesse du joueur
        rb.velocity = Vector3.forward * speed - new Vector3(rb.velocity.x,fallSpeed,rb.velocity.z);
        ExitCloud();
    }

    delegate void Origami();

    //Changement d'origami
    [ContextMenu("Change Origami")]
    public void ChangeOrigami()
    {
        // Update le changement d'état
        PlayerManager.instance.changeOrigami(); 
        boxCollider.center = new Vector3(boxCollider.center.x, PlayerManager.origami[PlayerManager.state].colliderCenter, boxCollider.center.z);
        boxCollider.size = PlayerManager.origami[PlayerManager.state].collider;
        
        //Quand le joueur ne décolle pas
        if (speed == PlayerManager.origami[PlayerManager.Shapes.BOAT].speed || speed == PlayerManager.origami[PlayerManager.Shapes.PLANE].speed || speed == 0)
        {
            fallSpeed = PlayerManager.origami[PlayerManager.state].fallSpeed;

            #region Boat speed

            if (PlayerManager.state == PlayerManager.Shapes.BOAT)
            {
                origamiSpeed = 0;
            }
            
            #endregion

            #region Plane speed

            else
            {
                origamiSpeed = PlayerManager.origami[PlayerManager.state].speed;
            }
            
            #endregion
        }

        else
        {
            origamiSpeed = speed;
        }
    }

    //Décollage quand le joueur sort du nuage
    void ExitCloud()
    {
        if (!isExitCloud)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseFov, Time.deltaTime);
            return;
        }

        #region Plane

        if (PlayerManager.state == PlayerManager.Shapes.PLANE)
        {
            if (speed>PlayerManager.origami[PlayerManager.state].speed)
            {
                speed -= reduceSpeed;
                fallSpeed = ascendSpeed;
            }
            else
            {
                speed = PlayerManager.origami[PlayerManager.state].speed;
                fallSpeed = PlayerManager.origami[PlayerManager.state].fallSpeed;
                isExitCloud = false;
            }
            
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, Time.deltaTime);
        }
        
        #endregion

        #region Boat
        
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseFov, Time.deltaTime);
            fallSpeed = PlayerManager.origami[PlayerManager.state].fallSpeed;
            if (speed>0)
            {
                speed -= reduceSpeed;
            }
            else
            {
                speed = 0;
                isExitCloud = false;
            }
        }
        
        #endregion
    }
}
