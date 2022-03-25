using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Touch touch;
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
            PlayerController.instance.Controller(difference,startTouchPosition);
            if (touch.phase == TouchPhase.Moved)
            {
                difference = (touch.position.x-(Screen.width/2))/Screen.width - startTouchPosition;
                
                UIManager.instance.FollowController(touch.position);
            }

            if (touch.phase == TouchPhase.Began)
            {
                //Debug.Log("Ui : "+touch.position + "World : "+cam.ScreenToWorldPoint(touch.position).x);
                UIManager.instance.SpawnControllers(touch.position);
                startTouchPosition = (touch.position.x - (Screen.width / 2)) / Screen.width;
                //startTouchPosition = cam.ScreenToWorldPoint(touch.position).x - startTouchPosition;
                difference = startTouchPosition;
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
