using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    #region Declarations

    [SerializeField] private GameObject shadow;
    
    [SerializeField] private LayerMask layer;
    private RaycastHit hit;
    
    #endregion


    private void FixedUpdate()
    {
        Shadow();
    }

    void Shadow()
    {
        if (Physics.Raycast(transform.position,Vector3.down,out hit,Mathf.Infinity,layer))
        {
            shadow.transform.position = hit.point;
        }
    }
}
