using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flesh : MonoBehaviour
{
    [SerializeField] private  Rigidbody rb;
    public float size;
    public Color color;
    void Awake()
    {
        rb.mass = rb.mass * size;
        this.GetComponent<Renderer>().material.color = color;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < 10)
        {
            rb.AddForce(Vector3.up * rb.mass * 9.66f, ForceMode.Force);
        }

        if(transform.position.z != 10)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, 10f), transform.rotation);
        }
    }
}
