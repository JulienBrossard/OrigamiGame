using UnityEngine;

public class InputManagerMainMenu : MonoBehaviour
{
    private Touch touch;
    float startTouchPosition; //Position du premier toucher

    float difference; //Détecte si le joueur va à gauche ou à droite
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(Input.touchCount - 1);

            #region Began

            if (touch.phase == TouchPhase.Began && !Tools.instance.IsPointerOverUI())
            {
                startTouchPosition = touch.position.x / Screen.width;
            }

            #endregion

            #region Ended
            
            if (touch.phase == TouchPhase.Ended)
            {
                difference = (touch.position.x)/Screen.width - startTouchPosition;
            }
            
            #endregion
        }
    }
}
