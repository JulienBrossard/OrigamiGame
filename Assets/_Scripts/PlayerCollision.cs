using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private Scene scene;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Cloud") && !other.gameObject.CompareTag("Paper Piece"))
        {
            scene = SceneManager.GetActiveScene();
            if (scene.name == "Tutorial") //Si dans tuto active la fonction retry
            {
                TutorialManager.instance.Retry();
            }
            else //Sinon failed
            {
                GameManager.instance.Failed();
            }
        }
    }
}
