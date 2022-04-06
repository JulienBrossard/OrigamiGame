using UnityEngine;

public class Collect : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<ICollectible>()!=null)
        {
            other.gameObject.GetComponent<ICollectible>().Collect();
        }
    }
}
