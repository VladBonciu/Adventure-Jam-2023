using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish: MonoBehaviour {

   
    public int maxSize;
    public bool carnivorous;
    public bool herbivorous;
    public float hunger;
    public float hungerCop;
    public float hungerSpeed;
    Rigidbody rb;
    private void Start()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        this.gameObject.transform.localScale = new Vector3(0.30f,0.30f,0.30f) * Random.Range(1, maxSize);
        herbivorous = RandomPreference();
        carnivorous = RandomPreference();
        hungerCop = hunger;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (transform.position.y < 10)
        {
            rb.AddForce(Vector3.up * rb.mass * 9.81f, ForceMode.Force);
        }
    }
    private void Update()
    {
        if (hunger == 0) 
        {
            Die();
        }
        
    }
    void Eat()
    {
    
    }
 bool RandomPreference() //Random bool generator
{
    if (Random.value >= 0.5)
    {
        return true;
    }
    return false;
}
    void Die() //Die if hungry
    {
    Destroy(gameObject);
    }
}
