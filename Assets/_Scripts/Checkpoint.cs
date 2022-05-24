using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform checkpoint;
    [SerializeField] private GameObject go;

    private void OnTriggerEnter(Collider other)
    {
        if (!TutorialManager.instance.isSucceed)
        {
            TutorialManager.instance.Retry();
        }
        else
        {
            TutorialManager.instance.EnableObject(go);
            TutorialManager.instance.NextCheckpoint(checkpoint);
        }
    }
}
