using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Declarations

    public static AnimationManager instance;
    
    public Animator playerAnimator;
    [SerializeField] private RuntimeAnimatorController origamiTransition;
    [SerializeField] private RuntimeAnimatorController planeAnim;
    
    #endregion

    private void Awake()
    {
        instance = this;
    }

    public void OrigamiTransition(bool isPlane)
    {
        #region Transition plane to boat
        
        if (!isPlane)
        {
            playerAnimator.runtimeAnimatorController = origamiTransition;
            playerAnimator.SetInteger("TransitionIndex",1);
        }
        
        #endregion

        #region Transition boat to plane
        
        else
        {
            playerAnimator.runtimeAnimatorController = origamiTransition;
            playerAnimator.SetInteger("TransitionIndex",2);
        }
        #endregion
        
    }


    public void Idle()
    {
        if (PlayerMovement.instance.isPlane)
        {
            playerAnimator.runtimeAnimatorController = planeAnim;
            playerAnimator.SetBool("isIdle",true);
        }
    }

    public void Movement(float direction)
    {
        if (PlayerMovement.instance.isPlane)
        {
            playerAnimator.runtimeAnimatorController = planeAnim;
            playerAnimator.SetBool("isIdle",false);
            playerAnimator.SetFloat("Movement",direction);
        }
    }
}
