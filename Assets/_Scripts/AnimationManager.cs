using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Declarations

    public static AnimationManager instance;
    
    [Header("Animator")]
    public Animator playerAnimator;
    
    [Header("Animator Controllers")]
    [SerializeField] private RuntimeAnimatorController origamiTransition;
    [SerializeField] private RuntimeAnimatorController planeAnim;
    
    #endregion

    private void Awake()
    {
        instance = this;
    }

    // Animation de changement d'Origami
    public void OrigamiTransition()
    {
        playerAnimator.runtimeAnimatorController = origamiTransition;
        playerAnimator.SetInteger("TransitionIndex",PlayerManager.origami[PlayerManager.state].transitionAnimationIndex);
    }

    // Animation statique de notre joueur
    public void Idle()
    {
        if (PlayerManager.state == PlayerManager.Shapes.PLANE)
        {
            playerAnimator.runtimeAnimatorController = planeAnim;
            playerAnimator.SetBool("isIdle",true);
        }
    }

    // ANimation de mouvement de l'origami
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
