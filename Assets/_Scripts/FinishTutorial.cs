using UnityEngine;

public class FinishTutorial : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.Failed(); //Si arrive à la ligne d'arrivée
    }
}
