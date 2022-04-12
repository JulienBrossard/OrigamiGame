using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Cloud") && !other.gameObject.CompareTag("Paper Piece"))
        {
            GameManager.instance.Failed();
        }
    }
}
