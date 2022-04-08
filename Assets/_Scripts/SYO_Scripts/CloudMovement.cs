using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float cloudSpeed = 5f;
    public string direction;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(direction == "Player")
        {
            transform.Translate(Vector3.up * cloudSpeed * Time.deltaTime);
        }

        if (direction == "Right")
        {
            transform.Translate(Vector3.right * cloudSpeed * Time.deltaTime);
        }

        if (direction == "Left")
        {
            transform.Translate(Vector3.left * cloudSpeed * Time.deltaTime);
        }

        if (direction == "Forward")
        {
            transform.Translate(Vector3.down * cloudSpeed * Time.deltaTime);
        }
    }
}
