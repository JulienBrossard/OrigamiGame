using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float cloudSpeedX = 5f; //Only affects goRight & goLeft
    public float cloudSpeedZ = 5f; //Only affects goPlayer & goForward
    public bool goPlayer, goForward, goLeft, goRight;
    

    void Update()
    {
        if(goPlayer)
        {
            transform.Translate(Vector3.up * cloudSpeedZ * Time.deltaTime);
        }

        if (goRight)
        {
            transform.Translate(Vector3.right * cloudSpeedX * Time.deltaTime);
        }

        if (goLeft)
        {
            transform.Translate(Vector3.left * cloudSpeedX * Time.deltaTime);
        }

        if (goForward)
        {
            transform.Translate(Vector3.down * cloudSpeedZ * Time.deltaTime);
        }
    }
}
