using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float movementTime;

    bool moving;
    public bool isTargeting;
    float movex;
    float movey;
    
    Vector3 target;
    public Transform mesh;
    Rigidbody rb;

    public Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isTargeting = false;

        transform.position = new Vector3(transform.position.x, transform.position.y, 10f);

        StartCoroutine(Move());
    }


    void FixedUpdate()
    {
        //Mesh Rotation
        Quaternion toRotation = Quaternion.LookRotation(-target, Vector3.up);

        mesh.rotation = Quaternion.RotateTowards(mesh.rotation, toRotation, 200 * Time.deltaTime);

        if(moving)
        {
            animator.SetBool("isSwimming", true);
            // transform.position = Vector3.Lerp(transform.position, new Vector3(movex ,movey , 10), Time.deltaTime * 0.1f);

            target = new Vector3(movex ,movey , 0).normalized;
            // rb.MovePosition(Vector3.LerpUnclamped(transform.position, new Vector3(movex ,movey , 10f), Time.deltaTime * 0.1f));
            rb.AddForce(new Vector3(movex ,movey , 10f).normalized * 0.2f, ForceMode.Force);
            
        }
        else
        {
            animator.SetBool("isSwimming", false);
        }
    }
    IEnumerator Move() 
    {
        moving = true;

        
        
        yield return new WaitForSeconds(movementTime);

        moving = false;

        movex = Random.Range(-20, 20);
        movey = Random.Range(-10, 10);
        movementTime = Random.Range(1, 7);
        
        

        yield return new WaitForSeconds(movementTime);

        if(!isTargeting)
        {
            StartCoroutine(Move());
        }
    }

    public void Target(Transform location)
    {
        isTargeting = true;
        target = location.position;
        Debug.Log("Start Targeting");
    }


}
