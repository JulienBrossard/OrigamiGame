using DG.Tweening;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Declarations

    public static AnimationManager instance;
    
    [Header("Animator")]
    [Tooltip("L'animator du joueur, à prendre directement sur le joueur")]
    public Animator playerAnimator;
    
    [Header("Animator Controllers")]
    [Tooltip("L'animator controller de la transition d'origami, à récupérer dans le dossier Animation")]
    [SerializeField] private RuntimeAnimatorController origamiTransition;
    [Tooltip("L'animator controller des mouvements de l'avion, à récupérer dans le dossier Animation")]
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

    public void DoMove(Transform obj, float translate, float speed)
    {
        obj.DOMove(obj.position + Vector3.right *translate, speed).SetEase(Ease.OutBack, speed).SetUpdate(true);
    }
    
    public void DoScale(Transform obj)
    {
        obj.DOScale(obj.transform.localScale + Vector3.one * 0.5f, 0.2f).OnComplete((() => obj.DOScale(obj.transform.localScale - Vector3.one * 0.5f, 0.2f)));
    }
}
