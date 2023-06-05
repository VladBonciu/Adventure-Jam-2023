using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisherman : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementRange;
    [SerializeField] GameObject hook;
    float maxDepth;
    int layerMask = 1 << 8;
 
    void Start()
    {
        
    }

   
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask)) 
        {
            maxDepth = hit.transform.position.y;
        }
        StartCoroutine(Move());
    }

    IEnumerator Move() 
    {
        rb.MovePosition(new Vector3(Random.Range(-movementRange, movementRange), transform.position.y, transform.position.z));
        DeployHook();
        yield return new WaitForSeconds(60);
        StartCoroutine(Move());       
    }

    void DeployHook() 
    {
        Instantiate(hook, new Vector3(transform.position.x,Random.Range(maxDepth+1,8),transform.position.z),Quaternion.identity);
    }
    

}
