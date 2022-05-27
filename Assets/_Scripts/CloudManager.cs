using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] private Transform movableCloudParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && movableCloudParent != null)
        {
            for (int i = 0; i < movableCloudParent.childCount; i++)
            {
                movableCloudParent.GetChild(i).GetComponent<CloudMovement>().enabled = true; //Active les nuages dynamiques lorsque l'on rentre dans un nouveau LD
            }
        }
    }
}
