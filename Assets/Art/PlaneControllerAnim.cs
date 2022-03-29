using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControllerAnim : MonoBehaviour
{
    public Animator planeAnim;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimationManagement();
    }


    private void AnimationManagement()
    {
        planeAnim.SetFloat("Movement", Input.GetAxis("Horizontal"));
    }
}
