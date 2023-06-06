using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform mesh;

    [SerializeField] private float moveSpeed;
    [SerializeField] float rotationSpeed;
    [HideInInspector] public bool canDash;
    [HideInInspector] public bool isMoving;
    [SerializeField] private float dashCountdown;

    [HideInInspector] public Vector3 moveDirection;

    [Header("KeyCodes")]

    [SerializeField] public KeyCode dashKey;

    [Header("Effects")]
    [SerializeField] GameObject fleshPrefab;

    void Awake()
    {
        canDash = true;
    }

    void FixedUpdate()
    {
        //Floating
        if(transform.position.y < 10)
        {
            rb.AddForce(Vector3.up * rb.mass * 9.81f , ForceMode.Force);
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, 0.0275f, Time.deltaTime * 2f);
        }
        else
        {
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity,  0.017f, Time.deltaTime * 3f);
        }

        //Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, verticalInput , 0f);

        rb.AddForce(moveDirection.normalized * moveSpeed * Mathf.Sin(Time.deltaTime * 50f), ForceMode.Force);
        
        //If is moving
        if(moveDirection != Vector3.zero)
        {
            isMoving = true;

            //Mesh Rotation
            Quaternion toRotation = Quaternion.LookRotation(-moveDirection.normalized, Vector3.up);

            mesh.rotation = Quaternion.RotateTowards(mesh.rotation, toRotation, rotationSpeed * Time.deltaTime);

            //Dash
            if(Input.GetKeyDown(dashKey) && canDash)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed , ForceMode.Impulse);
                StartCoroutine(DashCountDown());
            }
        }    
        else
        {
            isMoving = false;
        }
    }

    public void Die() //Die if hungry
    {
        GameObject flesh = fleshPrefab;
        flesh.transform.localScale = new Vector3(1f, 1f, 1f) * 3f * 2f;
        flesh.gameObject.GetComponent<Flesh>().color = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        flesh.gameObject.GetComponent<Flesh>().size = 3f;
        Instantiate(flesh, transform.position + Vector3.one * Random.Range(-.1f, .1f), transform.rotation);
        Instantiate(flesh, transform.position + Vector3.one * Random.Range(-.1f, .1f), transform.rotation);
        Instantiate(flesh, transform.position + Vector3.one * Random.Range(-.1f, .1f), transform.rotation);
         
        Destroy(gameObject);
    }

    IEnumerator DashCountDown()
    {
        yield return new WaitForSeconds(.01f);
        canDash = false;
        yield return new WaitForSeconds(dashCountdown);
        canDash = true;
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Fish") )
        {
            
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Vegetation") )
        {
            Destroy(collision.gameObject);
        }
    }
}
