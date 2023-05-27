using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance {get; private set;}

    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject mesh;

    [SerializeField] private float moveSpeed;
    [SerializeField] float rotationSpeed;

    public Vector3 moveDirection;

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
    }

    void FixedUpdate()
    {
        if(transform.position.y < 10)
        {
            rb.AddForce(Vector3.up * rb.mass * 9.81f , ForceMode.Force);
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, verticalInput , 0f);

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
        

        if(moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(-moveDirection, Vector3.up);

            mesh.transform.rotation = Quaternion.RotateTowards(mesh.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        
    }
}
