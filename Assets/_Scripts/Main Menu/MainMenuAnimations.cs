using DG.Tweening;
using UnityEngine;

public class MainMenuAnimations : MonoBehaviour
{
    [SerializeField] private Transform decors;
    [SerializeField] private Animator cloudAnimator;


    private void Start()
    {
        decors.DOLocalMoveY(Screen.height/4, 2).SetEase(Ease.OutBack, 2).SetUpdate(true).OnComplete((() =>
        {
            Decors();
        }));
    }

    public void Decors()
    {
        cloudAnimator.SetBool("VerticalAnimation",true);
    }
}
