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

    FishMovement fishMovement;

    private void Awake()
    {
        fishMovement = this.GetComponent<FishMovement>();

        carnivorous = RandomPreference();

        //If not carnivorous its herbivorous
        if(carnivorous)
        {
            herbivorous = RandomPreference();
        }
        else
        {
            herbivorous = true;
        }

        if(carnivorous)
        {
            this.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
        else
        {
            this.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.2f, 0.5f, 0.8f, 1f);
        }
        
        this.gameObject.transform.localScale = new Vector3(0.30f,0.30f,0.30f) * Random.Range(1, maxSize);

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
        hunger -= 1*Time.deltaTime;
        if (hunger == 0) 
        {
            Die();
        }
        
    }
    void Eat()
    {
        hunger = hungerCop;
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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Fish") && carnivorous)
        {
            
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Vegetation") && herbivorous)
        {
            Eat();
            Destroy(collision.gameObject);
        }
    }
}
