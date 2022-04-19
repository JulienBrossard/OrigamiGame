using UnityEngine;

public class ExternalForce : MonoBehaviour
{
    #region Declarations

    [SerializeField] private Vector3 externalForce;
    
    #endregion
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.externalForce += externalForce;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.externalForce -= externalForce;
        }
    }
}
