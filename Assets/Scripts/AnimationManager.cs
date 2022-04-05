using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;

    public Animator playerAnimator;
    [SerializeField] private RuntimeAnimatorController origamiTransition;
    [SerializeField] private RuntimeAnimatorController planeAnim;

    private void Awake()
    {
        instance = this;
    }

    public void OrigamiTransition(bool isPlane)
    {
        if (!isPlane)
        {
            playerAnimator.runtimeAnimatorController = origamiTransition;
            playerAnimator.SetInteger("TransitionIndex",1);
        }
        else
        {
            playerAnimator.runtimeAnimatorController = origamiTransition;
            playerAnimator.SetInteger("TransitionIndex",2);
        }
    }


    public void Idle()
    {
        playerAnimator.runtimeAnimatorController = planeAnim;
        playerAnimator.SetBool("isIdle",true);
    }

    public void Movement(float direction)
    {
        playerAnimator.runtimeAnimatorController = planeAnim;
        playerAnimator.SetBool("isIdle",false);
        playerAnimator.SetFloat("Movement",direction);
    }
}
