using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimations : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] private Transform decors;
    [SerializeField] private Image fade;
    
    [Header("Animator")]
    [SerializeField] private Animator cloudAnimator;


    private void Start()
    {
        fade.DOFade(0, 1.0f) // Anim du fade
            .OnComplete(() =>
            (decors.DOLocalMoveY(Screen.height / 4, 2).SetEase(Ease.OutBack, 2).SetUpdate(true)) // Anim du décors qui translate vers le bas
            .OnComplete(() => 
            Decors()) // Lancement de l'animation haut-Bas du Décors
            .OnComplete(() => 
                fade.gameObject.SetActive(false) )); // Désactivation de l'objet du fade
    }

    public void Decors()
    {
        cloudAnimator.SetBool("VerticalAnimation",true); // Lancement de l'anim dans l'animator
    }
}
