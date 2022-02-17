using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Touch touch;
    [SerializeField] private float sensibility;
    [SerializeField] private bool testGyro;
    [SerializeField] private float epsilon;

    float startTouchPosition;

    float difference;
    

    private void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        if (Input.touchCount > 0 && !testGyro)
        {
            touch = Input.GetTouch(Input.touchCount - 1);
            if (touch.phase == TouchPhase.Moved)
            {
                difference = (touch.position.x-(Screen.width/2))/Screen.width*sensibility - startTouchPosition;
                PlayerController.instance.Controller(difference);
                UIManager.instance.FollowController(touch.position);
            }

            if (touch.phase == TouchPhase.Began)
            {
                UIManager.instance.SpawnControllers(touch.position);
                startTouchPosition = (touch.position.x - (Screen.width / 2)) / Screen.width * sensibility;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                UIManager.instance.DePopControllers();
            }
        }

        if (testGyro)
        {
            Debug.Log(Math.Round(Input.gyro.rotationRate.y, 2));
            if ((Math.Round(Input.gyro.attitude.y,2)+0.01f)>0+epsilon || (Math.Round(Input.gyro.attitude.y,2)+0.01f)<0-epsilon)
            {
                PlayerController.instance.Gyro(Mathf.Sign(Input.gyro.attitude.y));
            }
        }
    }
}
