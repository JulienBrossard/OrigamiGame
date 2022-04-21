using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CloudCollision : MonoBehaviour
{

    #region Declarations
    
    private PlayerMovement playerMovement;
    
    [Header("Collider")]
    [SerializeField] private BoxCollider boxCollider;
    
    private enum Cloud
    {
        CLOUD,
        RAINYCLOUD
    }

    [Header("Type of cloud")]
    [SerializeField] private Cloud cloud;

    #endregion

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (!playerMovement.isCloud)
            {
                //Reset les variables
                playerMovement.isExitCloud = false;
                playerMovement.isCloud = true;
                playerMovement.cloudBoxCollider = boxCollider;

                #region Plane

                //L'avion passe à travers le nuage
                if (PlayerManager.state == PlayerManager.Shapes.PLANE)
                {
                    boxCollider.isTrigger = true;
                }
                
                #endregion

                #region Boat
                
                //Le bateau se pose sur le nuage
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
            //Reset les variables
            playerMovement.isExitCloud = false;
            playerMovement.isCloud = true;

            //Ajoute de la vitesse en fonction du type de nuage
            #region Rainy Cloud
            
            if (cloud == Cloud.RAINYCLOUD)
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
        //Reset les variables
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement.isExitCloud = true;
            playerMovement.isCloud = false;
        }
    }
}
