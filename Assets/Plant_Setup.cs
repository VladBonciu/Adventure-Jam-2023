using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Setup : MonoBehaviour
{
    // Start is called before the first frame update
    int layerMask = 1 << 8;
    int hitDistance = 1000;
    private void Awake()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down),out hit,Mathf.Infinity, layerMask)) 
        {
            Debug.Log("GroundHit");
       transform.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
