using System;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float gyroSpeed;
    [SerializeField] private float maxHorizontalPosition;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        maxHorizontalPosition = LevelManager.instance.maxHorizontalPosition;
    }

    public void Controller(float positionX)
    {
        if (Time.timeScale!=0)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + positionX,-maxHorizontalPosition,maxHorizontalPosition), transform.position.y, transform.position.z);
        }
    }

    public void Gyro(float sign)
    {
        rb.velocity = rb.velocity + transform.right*gyroSpeed*sign;
    }
}
