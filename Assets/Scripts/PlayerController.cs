using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance {get; private set;}

    [Header("Movement")]

    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform mesh;

    [SerializeField] private float moveSpeed;
    [SerializeField] float rotationSpeed;
    [HideInInspector] public bool canDash;
    [SerializeField] private float dashCountdown;

    [HideInInspector] public Vector3 moveDirection;

    [Header("KeyCodes")]

    [SerializeField] public KeyCode dashKey;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        canDash = true;
    }

    void FixedUpdate()
    {
        //Floating
        if(transform.position.y < 10)
        {
            rb.AddForce(Vector3.up * rb.mass * 9.81f , ForceMode.Force);
        }

        //Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, verticalInput , 0f);

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
        
        //If is moving
        if(moveDirection != Vector3.zero)
        {
            //Mesh Rotation
            Quaternion toRotation = Quaternion.LookRotation(-moveDirection, Vector3.up);

            mesh.rotation = Quaternion.RotateTowards(mesh.rotation, toRotation, rotationSpeed * Time.deltaTime);

            //Dash
            if(Input.GetKeyDown(dashKey) && canDash)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed , ForceMode.Impulse);
                StartCoroutine(DashCountDown());
            }
        }    
    }

    IEnumerator DashCountDown()
    {
        yield return new WaitForSeconds(.01f);
        canDash = false;
        yield return new WaitForSeconds(dashCountdown);
        canDash = true;
    }
}
