using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Touch touch;

    float startTouchPosition;

    float difference;
    

    private void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log(!Tools.instance.IsPointerOverUI());
            touch = Input.GetTouch(Input.touchCount - 1);
            if (touch.phase == TouchPhase.Moved)
            {
                difference = (touch.position.x-(Screen.width/2))/Screen.width - startTouchPosition;
                
                UIManager.instance.FollowController(touch.position);
                AnimationManager.instance.Movement(difference);
                PlayerController.instance.Controller(difference,startTouchPosition);
            }

            if (touch.phase == TouchPhase.Began && !Tools.instance.IsPointerOverUI())
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
                if (!Tools.instance.IsPointerOverUI())
                {
                    AnimationManager.instance.Idle();
                }
            }
        }
    }
}
