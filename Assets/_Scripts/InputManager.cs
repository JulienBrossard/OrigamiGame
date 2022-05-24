using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Declarations
    
    private Touch touch;

    float startTouchPosition; //Position du premier toucher

    float difference; //Détecte si le joueur va à gauche ou à droite

    private bool isMoved;

    #endregion
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(Input.touchCount - 1);

            #region Moved
            
            if (touch.phase == TouchPhase.Moved)
            {
                difference = (touch.position.x)/Screen.width - startTouchPosition; //ScreenToWorldPoint ne fontionne qu'avec cam ortho sur mobile

                #region UI
                
                //UI du controller qui suit le joueur
                UIManager.instance.FollowController(touch.position);
                
                #endregion

                #region Animation
                
                //Animation de mouvement
                AnimationManager.instance.Movement(difference);
                
                #endregion
            }
            
            #endregion

            #region Began

            if (touch.phase == TouchPhase.Began && !Tools.instance.IsPointerOverUI())
            {
                #region UI
                
                //Spawn de l'UI du controller
                UIManager.instance.SpawnControllers(touch.position);

                #endregion

                startTouchPosition = touch.position.x / Screen.width;

                isMoved = true;
            }

            #endregion

            #region Ended
            
            if (touch.phase == TouchPhase.Ended)
            {
                #region UI
                
                //Depop l'UI du controller
                UIManager.instance.DePopControllers();
                
                #endregion

                #region Animation
                
                //Joue l'animation seulement si le joueur n'appuie pas sur une UI
                if (!Tools.instance.IsPointerOverUI())
                {
                    AnimationManager.instance.Idle();
                }
                
                #endregion
                
                //Reset les variables
                difference = 0;
                startTouchPosition = 0;
                isMoved = false;
            }
            
            #endregion
        }
    }

    private void FixedUpdate()
    {
        if (isMoved)
        {
            PlayerController.instance.Controller(difference);
        }
    }
}
