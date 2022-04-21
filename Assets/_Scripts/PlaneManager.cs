using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    #region Declarations

    [Header("Shadow")]
    [SerializeField] private GameObject shadow;
    
    [Header("Layer")]
    [SerializeField] private LayerMask layer;
    private RaycastHit hit;
    
    #endregion


    private void FixedUpdate()
    {
        Shadow();
    }

    //Update de la position de l'ombre
    void Shadow()
    {
        if (Physics.Raycast(transform.position,Vector3.down,out hit,Mathf.Infinity,layer))
        {
            shadow.transform.position = hit.point;
        }
    }
}
