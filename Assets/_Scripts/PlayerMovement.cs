using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    
    [SerializeField] private Rigidbody rb;
    
    public float speed;
    [SerializeField] private float planeSpeed;
    public float boatSpeed;
    public float boostSpeed;
    public float boostRainSpeed;
    [SerializeField] private float reduceSpeed;
    
    public float fallSpeed;
    [SerializeField] private float planeFallSpeed;
    public float boatFallSpeed;
    [SerializeField] private float ascendSpeed;

    public bool isPlane;
    
    [SerializeField] private TextMeshProUGUI origamiText;

    public BoxCollider cloudBoxCollider;

    public bool isExitCloud;
    public bool isCloud;

    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Vector3 planeCollider;
    [SerializeField] private Vector3 boatCollider;
    [SerializeField] private float planeCenter;
    [SerializeField] private float boatCenter;

    [SerializeField] private float fov;

    private void Awake()
    {
        instance = this;
        speed = planeSpeed;
        fallSpeed = planeFallSpeed;
    }

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
    
    void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed - new Vector3(rb.velocity.x,fallSpeed,rb.velocity.z);
        ExitCloud();
    }

    void UpdateText()
    {
        if (isPlane)
        {
            origamiText.text = "Boat";
        }

        else
        {
            origamiText.text = "Plane";
        }
    }

    public void ChangeOrigami()
    {
        if (speed == boatSpeed || speed == planeSpeed || speed == 0)
        {
            if (isPlane)
            {
                origamiSpeed = 0;
                fallSpeed = boatFallSpeed;
                boxCollider.center = new Vector3(boxCollider.center.x, boatCenter, boxCollider.center.z);
                boxCollider.size = boatCollider;
            }

            else
            {
                origamiSpeed = planeSpeed;
                fallSpeed = planeFallSpeed;
                boxCollider.center = new Vector3(boxCollider.center.x, planeCenter, boxCollider.center.z);
                boxCollider.size = planeCollider;
            }
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
    }
}
