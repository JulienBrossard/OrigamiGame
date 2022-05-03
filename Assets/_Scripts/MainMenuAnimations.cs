using DG.Tweening;
using UnityEngine;

public class MainMenuAnimations : MonoBehaviour
{
    [SerializeField] private Transform decors;

    private void Start()
    {
        decors.DOLocalMoveY(Screen.height/4, 2).SetEase(Ease.OutBack, 2).SetUpdate(true);
    }
}
