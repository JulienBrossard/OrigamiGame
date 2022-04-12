using UnityEngine;

public class CloudCollision : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private bool rainyCloud;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (!playerMovement.isCloud)
            {
                playerMovement.isExitCloud = false;
                playerMovement.isCloud = true;
                playerMovement.cloudBoxCollider = boxCollider;
                if (playerMovement.isPlane)
                {
                    boxCollider.isTrigger = true;
                }
                else
                {
                    playerMovement.speed = playerMovement.boatSpeed;
                    playerMovement.fallSpeed = 0;
                }
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement.isExitCloud = false;
            playerMovement.isCloud = true;
            if (rainyCloud)
            {
                playerMovement.speed += playerMovement.boostRainSpeed;
            }
            else
            {
                playerMovement.speed += playerMovement.boostSpeed;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement.isExitCloud = true;
            playerMovement.isCloud = false;
        }
    }
}
