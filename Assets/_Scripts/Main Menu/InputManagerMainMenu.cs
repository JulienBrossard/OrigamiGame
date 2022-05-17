using UnityEngine;

public class InputManagerMainMenu : MonoBehaviour
{
    private Touch touch;
    float startTouchPosition; //Position du premier toucher

    float difference; //Détecte si le joueur va à gauche ou à droite

    [SerializeField] private bool isChooseLevel;
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(Input.touchCount - 1);

            #region Began

            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.y>Screen.height/2) 
                {
                    isChooseLevel = true;
                    startTouchPosition = touch.position.x / Screen.width;
                }
            }

            #endregion

            #region Ended
            
            if (touch.phase == TouchPhase.Ended)
            {
                if (isChooseLevel)
                {
                    difference = (touch.position.x)/Screen.width - startTouchPosition;
                    isChooseLevel = false;
                    ChooseLevel.instance.Level(difference);
                }
            }
            
            #endregion
        }
    }
}
