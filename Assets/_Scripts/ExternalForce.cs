using UnityEngine;

public class ExternalForce : MonoBehaviour
{
    #region Declarations

    [Tooltip("La force de l'objet sur le joueur")]
    [SerializeField] private Vector3 externalForce;
    
    #endregion
    
    private void OnTriggerEnter(Collider other)
    {
        //Ajoute de la force quand le joueur rentre en collision avec l'objet
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.externalForce += -externalForce;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Supprime la force quand le joueur sort du point de contact avec l'objet
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.externalForce -= -externalForce;
        }
    }
}
