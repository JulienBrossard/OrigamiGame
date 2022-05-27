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

            if (touch.phase == TouchPhase.Began && Tools.instance.ObjectPointerOverUI().Count <= 1)
            {
                if (Tools.instance.ObjectPointerOverUI().Count == 0)
                {
                    startTouchPosition = touch.position.x / Screen.width;
                }
                else if (Tools.instance.ObjectPointerOverUI()[0].gameObject.name == "MainBackground")
                {
                    startTouchPosition = touch.position.x / Screen.width;
                }
            }

            #endregion

            #region Ended
            
            if (touch.phase == TouchPhase.Ended && Tools.instance.ObjectPointerOverUI().Count <= 1)
            {
                if (Tools.instance.ObjectPointerOverUI().Count == 0)
                {
                    difference = (touch.position.x)/Screen.width - startTouchPosition;
                    ChooseLevel.instance.Level(difference);
                }
                else if (Tools.instance.ObjectPointerOverUI()[0].gameObject.name == "MainBackground")
                {
                    difference = (touch.position.x)/Screen.width - startTouchPosition;
                    ChooseLevel.instance.Level(difference);
                }
                difference = 0;
                startTouchPosition = 0;
            }
            
            #endregion
        }
    }
}
