using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float movementTime;
    [SerializeField] private ParticleSystem swimBubbles;
    public float moveSpeed;

    bool wandering;
    float movex;
    float movey;
    bool isTargeting;
    Rigidbody rb;

    Vector3 direction;
    [SerializeField] private Transform meshGameObject;
    [SerializeField] private Animator animator;

    void Awake()
    {
        isTargeting = false;

        rb = GetComponent<Rigidbody>();

        transform.position = new Vector3(transform.position.x, transform.position.y, 10f);

        StartCoroutine(Wander());
    }

    void Update()
    {
        //Mesh Rotation
        Quaternion toRotation = Quaternion.LookRotation(-direction, Vector3.up);

        meshGameObject.rotation = Quaternion.RotateTowards(meshGameObject.rotation, toRotation, 200 * Time.deltaTime);

        if(transform.position.z != 10)
        {
            // rb.MovePosition(new Vector3(rb.position.x, rb.position.y, 10f));
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, 10f), transform.rotation);
        }
    }


    void FixedUpdate()
    {
        if(wandering)
        {
            animator.SetBool("isSwimming", true);

            direction = new Vector3(movex ,movey , 0).normalized;

            rb.AddForce(new Vector3(movex ,movey , 0f).normalized * moveSpeed * rb.mass * 3f, ForceMode.Force);

            if(!swimBubbles.isEmitting)
            {
                swimBubbles.Play();
            }            
        }
        else if(isTargeting)
        {
            animator.SetBool("isSwimming", true);

            rb.AddForce(direction * moveSpeed * rb.mass * 3f, ForceMode.Force);
        }
        else
        {
            animator.SetBool("isSwimming", false);
            swimBubbles.Stop(true , ParticleSystemStopBehavior.StopEmitting);
        }
    }
    IEnumerator Wander() 
    {
        wandering = true;
        
        yield return new WaitForSeconds(movementTime);

        wandering = false;

        movex = Random.Range(-20, 20);
        movey = Random.Range(-10, 10);
        movementTime = Random.Range(1, 5);
        

        yield return new WaitForSeconds(movementTime);

        if(!isTargeting)
        {
            StartCoroutine(Wander());
        }
        
    }

    public void TartgetObject(Transform location)
    {
        isTargeting = true;
        Vector3 heading = new Vector3(location.position.x ,location.position.y , 0) - new Vector3(transform.position.x ,transform.position.y , 0);
        direction = ( heading / heading.magnitude).normalized;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Fish") )
        {
            isTargeting = false;
            StartCoroutine(Wander());
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Vegetation") )
        {
            isTargeting = false;
            StartCoroutine(Wander());
        }
    }

}
