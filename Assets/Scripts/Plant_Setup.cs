using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Setup : MonoBehaviour
{
    int layerMask = 1 << 8;
    // int hitDistance = 1000;
    private void Awake()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down),out hit,Mathf.Infinity, layerMask)) 
        {
            transform.position = hit.point;
        }
    }

    void Update()
    {
        
    }
}
