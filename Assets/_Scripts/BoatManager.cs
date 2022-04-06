using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    [SerializeField] private GameObject shadow;
    [SerializeField] private LayerMask layer;

    private RaycastHit hit;


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
