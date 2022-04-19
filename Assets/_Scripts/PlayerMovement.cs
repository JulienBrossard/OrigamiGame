using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Declarations

    public static PlayerMovement instance;
    
    [SerializeField] private Rigidbody rb;

    #region Speeds

    public float speed;
    [SerializeField] private float planeSpeed;
    public float boatSpeed;
    public float boostSpeed;
    public float boostRainSpeed;
    [SerializeField] private float reduceSpeed;
    
    #endregion

    #region Fall Speeds

    public float fallSpeed;
    [SerializeField] private float planeFallSpeed;
    public float boatFallSpeed;
    [SerializeField] private float ascendSpeed;
    
    #endregion

    [HideInInspector] public bool isPlane;
    
    [SerializeField] private TextMeshProUGUI origamiText;

    #region Cloud Boolean

    [HideInInspector] public bool isExitCloud;
    [HideInInspector] public bool isCloud;
    
    #endregion

    #region Collider

    [HideInInspector] public BoxCollider cloudBoxCollider;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Vector3 planeCollider;
    [SerializeField] private Vector3 boatCollider;
    [SerializeField] private float planeCenter;
    [SerializeField] private float boatCenter;
    
    #endregion

    [SerializeField] private float fov;
    
    #endregion

    float origamiSpeed
    {
        get => speed;
        set
        {
            speed = value;
            isPlane = !isPlane;
            UpdateText();
            TriggerCloud();
            //plane.SetActive(!plane.activeSelf);
            //boat.SetActive(!boat.activeSelf);
            AnimationManager.instance.OrigamiTransition(isPlane);
        }
    }
    
    private void Awake()
    {
        instance = this;

        #region Initialize Speed

        speed = planeSpeed;
        fallSpeed = planeFallSpeed;
        
        #endregion
    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed - new Vector3(rb.velocity.x,fallSpeed,rb.velocity.z);
        ExitCloud();
    }

    void UpdateText()
    {
        #region Plane
        
        if (isPlane)
        {
            origamiText.text = "Boat";
        }
        
        #endregion

        #region Boat

        else
        {
            origamiText.text = "Plane";
        }
        
        #endregion
    }

    public void ChangeOrigami()
    {
        if (speed == boatSpeed || speed == planeSpeed || speed == 0)
        {
            #region Boat

            if (isPlane)
            {
                origamiSpeed = 0;
                fallSpeed = boatFallSpeed;
                boxCollider.center = new Vector3(boxCollider.center.x, boatCenter, boxCollider.center.z);
                boxCollider.size = boatCollider;
            }
            
            #endregion

            #region Plane

            else
            {
                origamiSpeed = planeSpeed;
                fallSpeed = planeFallSpeed;
                boxCollider.center = new Vector3(boxCollider.center.x, planeCenter, boxCollider.center.z);
                boxCollider.size = planeCollider;
            }
            
            #endregion
        }

        else
        {
            origamiSpeed = speed;
        }
    }

    void TriggerCloud()
    {
        if (cloudBoxCollider != null)
        {
            cloudBoxCollider.isTrigger = !cloudBoxCollider.isTrigger;
        }
    }

    void ExitCloud()
    {
        if (!isExitCloud)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime);
            return;
        }

        #region Plane

        if (isPlane)
        {
            if (speed>planeSpeed)
            {
                speed -= reduceSpeed;
                fallSpeed = ascendSpeed;
            }
            else
            {
                speed = planeSpeed;
                fallSpeed = planeFallSpeed;
                isExitCloud = false;
            }

            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, Time.deltaTime);
        }
        
        #endregion

        #region Boat
        
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime);
            fallSpeed = boatFallSpeed;
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
