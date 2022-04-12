using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxHorizontalPosition;
    [SerializeField] private float sensibility;
    public Vector3 externalForce;

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
            rb.velocity = new Vector3((startPositionX - positionX) * sensibility, rb.velocity.y, rb.velocity.z) - externalForce;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x,-maxHorizontalPosition,maxHorizontalPosition), transform.position.y, transform.position.z);
            //transform.position = new Vector3(Mathf.Clamp(transform.position.x + positionX,-maxHorizontalPosition,maxHorizontalPosition), transform.position.y, transform.position.z);
        }
    }
}
