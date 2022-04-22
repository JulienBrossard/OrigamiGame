using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Declarations
    
    private Touch touch;

    float startTouchPosition;

    float difference;
    
    #endregion

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(Input.touchCount - 1);

            #region Controller

            PlayerController.instance.Controller(difference,startTouchPosition);
            
            #endregion

            #region Moved
            
            if (touch.phase == TouchPhase.Moved)
            {
                difference = (touch.position.x-(Screen.width/2))/Screen.width - startTouchPosition;

                #region UI
                
                UIManager.instance.FollowController(touch.position);
                
                #endregion

                #region Animation
                
                AnimationManager.instance.Movement(difference);
                
                #endregion
            }
            
            #endregion

            #region Began

            if (touch.phase == TouchPhase.Began && !Tools.instance.IsPointerOverUI())
            {
                #region UI
                
                UIManager.instance.SpawnControllers(touch.position);
                
                #endregion
                
                startTouchPosition = (touch.position.x - (Screen.width / 2)) / Screen.width;
                difference = startTouchPosition;
            }

            #endregion

            #region Ended
            
            if (touch.phase == TouchPhase.Ended)
            {
                #region UI
                
                UIManager.instance.DePopControllers();
                
                #endregion

                #region Animation
                
                if (!Tools.instance.IsPointerOverUI())
                {
                    AnimationManager.instance.Idle();
                }
                
                #endregion

                difference = 0;
                startTouchPosition = 0;
            }
            
            #endregion
        }

        else
        {
            #region Controller

            PlayerController.instance.Controller(difference,startTouchPosition);
            
            #endregion
        }
    }
}
