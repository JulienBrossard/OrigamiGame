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

    public void OrigamiTransition()
    {
        playerAnimator.runtimeAnimatorController = origamiTransition;
        Debug.Log(PlayerManager.origami[PlayerManager.state].transitionAnimationIndex);
        playerAnimator.SetInteger("TransitionIndex",PlayerManager.origami[PlayerManager.state].transitionAnimationIndex);
    }


    public void Idle()
    {
        if (PlayerManager.state == PlayerManager.Shapes.PLANE)
        {
            playerAnimator.runtimeAnimatorController = planeAnim;
            playerAnimator.SetBool("isIdle",true);
        }
    }

    public void Movement(float direction)
    {
        if (PlayerManager.state == PlayerManager.Shapes.PLANE)
        {
            playerAnimator.runtimeAnimatorController = planeAnim;
            playerAnimator.SetBool("isIdle",false);
            playerAnimator.SetFloat("Movement",direction);
        }
    }
}
