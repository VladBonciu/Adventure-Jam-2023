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
    bool moving = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Move());
    }
    private void FixedUpdate()
    {

        // rb.AddForce(Vector3.up * rb.mass * Physics.gravity.y, ForceMode.Acceleration);
        // rb.MoveRotation(Quaternion.Euler(0f, 0f, 0f));

        Quaternion deltaRotation = Quaternion.Euler(0f, 0f, 0f);
        transform.rotation= Quaternion.Slerp(transform.rotation, deltaRotation, Time.deltaTime);  
        
        if (moving)
        {
            // transform.position = Vector3.Lerp(transform.position, new Vector3(Random.Range(-movementRange, movementRange), transform.position.y, transform.position.z), speed);
            rb.AddForce(new Vector3(Random.Range(-movementRange, movementRange), 0f, 0f).normalized * speed  * 4000f, ForceMode.Force); 
            moving = false;
        }
    }
    IEnumerator Move() 
    {
        moving = true;
        yield return new WaitForSeconds(5);
        moving = false;
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
        hookObject.GetComponent<Hook>().setHeight = Random.Range(3, maxDepth);

        Instantiate(hookObject, transform.position + Vector3.down , Quaternion.identity);
    }
   
    

}
