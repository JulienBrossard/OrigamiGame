using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    #region Declarations
    
    [SerializeField] private Rigidbody rb;

    [SerializeField] private TextMeshProUGUI origamiText;
    
    [SerializeField] private float fov;

    Origami origamiChangement;

    #region Horizontal Speeds

    [Header("Speeds", order = 1)]
    public float speed;
    public float boostSpeed;
    public float boostRainSpeed;
    [SerializeField] private float reduceSpeed;
    
    #endregion
    
    
    #region Vertical Speeds

    [Header("Fall Speeds", order =1)]
    public float fallSpeed;
    [SerializeField] private float ascendSpeed;
    
    #endregion
    
    
    #region Cloud Booleans
    [HideInInspector] public bool isExitCloud;
    [HideInInspector] public bool isCloud;
    
    #endregion
    

    #region Collider

    [Header("Colliders", order =1)]
    [SerializeField] private BoxCollider boxCollider;
    [HideInInspector] public BoxCollider cloudBoxCollider;
    
    #endregion

    #endregion

    float origamiSpeed
    {
        get => speed;
        set
        {
            speed = value;
            origamiChangement();
            //plane.SetActive(!plane.activeSelf);
            //boat.SetActive(!boat.activeSelf);
            AnimationManager.instance.OrigamiTransition();
        }
    }

    private void Start()
    {
        //Initialise le delegate
        origamiChangement += UpdateText;
        origamiChangement += TriggerCloud;
        
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

    //Update le texte afficher sur le bouton de changement d'origami
    void UpdateText()
    {
        origamiText.text = PlayerManager.origami[PlayerManager.state].name;
    }

    delegate void Origami();

    //Changement d'origami
    [ContextMenu("Change Origami")]
    public void ChangeOrigami()
    {
        // Update le changement d'état
        PlayerManager.instance.ChangeState(); 
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
    
    //Change l'état Trigger du dernier nuage touché
    void TriggerCloud()
    {
        if (cloudBoxCollider != null)
        {
            cloudBoxCollider.isTrigger = !cloudBoxCollider.isTrigger;
        }
    }

    //Décollage quand le joueur sort du nuage
    void ExitCloud()
    {
        if (!isExitCloud)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime);
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
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime);
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
