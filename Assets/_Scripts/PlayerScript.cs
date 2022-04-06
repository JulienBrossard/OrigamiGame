using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody PlaneRb;
    public GameObject Plane;
    public Animator playerAnim;
    public float speedPlane = 50f;

    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;
    public float horizontalSpeed = 10f;
    public float verticalSpeed = 10f;

    private int currentShapeIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlaneRb.velocity = new Vector3(horizontalMovement, verticalMovement, speedPlane);
        Move();
        Rotate();

        if(Input.GetKeyDown(KeyCode.T))
        {
            Transition();
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            verticalMovement += Time.deltaTime * verticalSpeed;
        }
        else
        {
            //verticalMovement -= Time.deltaTime * verticalSpeed;
        }

        if (Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Horizontal") < -0.1f)
        {
            horizontalMovement = Input.GetAxis("Horizontal") * horizontalSpeed;
        }
        else
        {
            horizontalMovement = 0f;
        }
    }

    private void Rotate()
    {
        if(currentShapeIndex<1)
        {
            float planeRotationRate;
            planeRotationRate = Mathf.Clamp(horizontalMovement * -2f, -20, 20);
            Plane.transform.rotation = Quaternion.Euler(new Vector3(0, 0, planeRotationRate));
        }
    }

    private void Transition()
    {
        currentShapeIndex++;
        playerAnim.SetInteger("TransitionIndex", currentShapeIndex);
        if(currentShapeIndex>1)
        {
            currentShapeIndex = 0;
        }
        Debug.Log(currentShapeIndex);
    }
}
