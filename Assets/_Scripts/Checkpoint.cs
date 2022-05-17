using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!TutorialManager.instance.isSucceed)
        {
            TutorialManager.instance.Retry();
        }
        else
        {
            TutorialManager.instance.NextCheckpoint(checkpoint);
        }
    }
}
