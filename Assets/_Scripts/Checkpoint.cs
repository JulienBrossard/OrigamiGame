using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform checkpoint;
    [SerializeField] private GameObject go;

    private void OnTriggerEnter(Collider other)
    {
        TutorialManager.instance.EnableObject(go); //Active l'objet
        TutorialManager.instance.NextCheckpoint(checkpoint); // Dernier checkpoint atteint
    }
}
