using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimations : MonoBehaviour
{
    [SerializeField] private Transform decors;
    [SerializeField] private Animator cloudAnimator;
    [SerializeField] private Image fade;


    private void Start()
    {
        fade.DOFade(0, 1.0f)
            .OnComplete(() =>
            (decors.DOLocalMoveY(Screen.height / 4, 2).SetEase(Ease.OutBack, 2).SetUpdate(true))
            .OnComplete(() => 
            Decors()));
    }

    public void Decors()
    {
        cloudAnimator.SetBool("VerticalAnimation",true);
    }
}
