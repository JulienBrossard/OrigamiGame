using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask layer;
    [SerializeField] private MeshRenderer mesh;

    private void Update()
    {
        if (Time.frameCount%3 == 0)
        {
            AlphaObject();
        }
    }

    void AlphaObject()
    {
        if (Physics.Raycast(transform.position,(player.position-transform.position).normalized,out hit,Vector3.Distance(transform.position,player.position),layer))
        {
            if (hit.transform.parent.GetChild(hit.transform.parent.childCount-1).GetInstanceID() != mesh.gameObject.GetInstanceID())
            {
                mesh.enabled = true;
                mesh = hit.transform.parent.GetChild(hit.transform.parent.childCount - 1).GetComponent<MeshRenderer>();
            }
            mesh.enabled = false;
        }
        else
        {
            mesh.enabled = true;
        }
    }
}
