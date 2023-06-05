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
    [SerializeField] float speed = 5;
    bool CanMove = false;
    public float hookingTime = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Move());
    }

   
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask)) 
        {
            maxDepth = hit.transform.position.y;
        }
     
    }

    IEnumerator Move() 
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Random.Range(-movementRange, movementRange), transform.position.y, transform.position.z), speed);
        yield return new WaitForSeconds(5);
        CanMove = false;
        DeployHook();
        yield return new WaitForSeconds(hookingTime);
        StartCoroutine(Move());       
    }

    void DeployHook() 
    {
        Instantiate(hook, new Vector3(transform.position.x,Random.Range(maxDepth+1,8),transform.position.z),Quaternion.identity);
    }
   
    

}
