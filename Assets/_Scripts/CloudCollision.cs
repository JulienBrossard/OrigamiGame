using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CloudCollision : MonoBehaviour
{

    #region Declarations
    
    private PlayerMovement playerMovement;
    
    [Header("Collider")]
    [Tooltip("Le boxCollider du nuage, à récupérer sur le nuage actuel")]
    [SerializeField] private BoxCollider boxCollider;
    
    private enum Cloud
    {
        CLOUD,
        RAINYCLOUD
    }

    [Header("Type of cloud")]
    [Tooltip("Le type de nuage, à changer en fonction du type de notre nuage")]
    [SerializeField] private Cloud cloud;

    [Header("Sounds")] [SerializeField] private AudioClip liftoffSound;
    #endregion

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (!playerMovement.isCloud)
            {
                AudioManager.instance.ChangeWindSound();
                
                //Reset les variables
                playerMovement.isExitCloud = false;
                playerMovement.isCloud = true;
                
                //PlayerManager.instance.cloudBoxCollider = boxCollider;

                #region Boat
                
                //Le bateau se pose sur le nuage
                if(PlayerManager.state == PlayerManager.Shapes.BOAT)
                {
                    if (playerMovement.speed < PlayerManager.origami[PlayerManager.state].speed)
                    {
                        playerMovement.speed = PlayerManager.origami[PlayerManager.state].speed;
                    }
                    playerMovement.fallSpeed = 0;
                    PlayerManager.instance.shadow.SetActive(false);
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
            AudioManager.instance.ChangeWindSound();
            playerMovement.isExitCloud = true;
            playerMovement.isCloud = false;
            PlayerManager.instance.shadow.SetActive(true);
            if (PlayerManager.state == PlayerManager.Shapes.PLANE)
            {
                AudioManager.instance.PlaySound(liftoffSound, 0.65f,1f,0);
            }
        }
        
    }
}
