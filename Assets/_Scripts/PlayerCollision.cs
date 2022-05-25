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
            if (scene.name == "Tutorial")
            {
                TutorialManager.instance.Retry();
            }
            else
            {
                GameManager.instance.Failed();
            }
        }
    }
}
