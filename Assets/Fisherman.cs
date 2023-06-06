using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisherman : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementRange;
    [SerializeField] GameObject hook;
    [SerializeField] private float maxDepth;
    [SerializeField] float speed = 5;
    bool CanMove = false;
    public float hookingTime = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Move());
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
        RaycastHit hit;

        if(Physics.Raycast(transform.position + Vector3.down, Vector3.down, out hit, 100f)) 
        {
            maxDepth = hit.distance;
            Debug.Log(hit.distance);
        }
        
        GameObject hookObject = hook;
        hookObject.GetComponent<Hook>().setHeight = transform.position.y - Random.Range(2 , maxDepth);

        Instantiate(hookObject, new Vector3(transform.position.x, transform.position.y - 1,transform.position.z),Quaternion.identity);
    }
   
    

}
