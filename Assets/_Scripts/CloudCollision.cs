using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CloudCollision : MonoBehaviour
{

    #region Declarations
    
    private PlayerMovement playerMovement;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private bool rainyCloud;

    #endregion

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

                #region Plane

                if (PlayerManager.state == PlayerManager.Shapes.PLANE)
                {
                    boxCollider.isTrigger = true;
                }
                
                #endregion

                #region Boat
                
                else
                {
                    playerMovement.speed = PlayerManager.origami[PlayerManager.state].speed;
                    playerMovement.fallSpeed = 0;
                }
                
                #endregion
                
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement.isExitCloud = false;
            playerMovement.isCloud = true;

            #region Rainy Cloud
            
            if (rainyCloud)
            {
                playerMovement.speed += playerMovement.boostRainSpeed;
            }
            
            #endregion

            #region Cloud
            
            else
            {
                playerMovement.speed += playerMovement.boostSpeed;
            }
            
            #endregion
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
