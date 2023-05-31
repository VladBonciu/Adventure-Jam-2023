using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float movementTime;
    [SerializeField] private ParticleSystem swimBubbles;
    public float moveSpeed;

    bool moving;
    float movex;
    float movey;
    Rigidbody rb;

    Vector3 target;
    [SerializeField] private Transform meshGameObject;
    [SerializeField] private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        transform.position = new Vector3(transform.position.x, transform.position.y, 10f);

        StartCoroutine(Wander());
    }

    void Update()
    {
        //Mesh Rotation
        Quaternion toRotation = Quaternion.LookRotation(-target, Vector3.up);

        meshGameObject.rotation = Quaternion.RotateTowards(meshGameObject.rotation, toRotation, 200 * Time.deltaTime);

        if(transform.position.z != 10)
        {
            // rb.MovePosition(new Vector3(rb.position.x, rb.position.y, 10f));
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, 10f), transform.rotation);
        }
    }


    void FixedUpdate()
    {
        if(moving)
        {
            animator.SetBool("isSwimming", true);

            target = new Vector3(movex ,movey , 0).normalized;

            rb.AddForce(new Vector3(movex ,movey , 0f).normalized * moveSpeed * rb.mass * 3f, ForceMode.Force);

            if(!swimBubbles.isEmitting)
            {
                swimBubbles.Play();
            }            
        }
        else
        {
            animator.SetBool("isSwimming", false);
            swimBubbles.Stop(true , ParticleSystemStopBehavior.StopEmitting);
        }
    }
    IEnumerator Wander() 
    {
        moving = true;
        
        yield return new WaitForSeconds(movementTime);

        moving = false;

        movex = Random.Range(-20, 20);
        movey = Random.Range(-10, 10);
        movementTime = Random.Range(1, 7);
        

        yield return new WaitForSeconds(movementTime);

        StartCoroutine(Wander());
    }


}
