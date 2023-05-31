using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish: MonoBehaviour {

    [Header("General")]
    public FishType fishType;
    [SerializeField] private FishMovement fishMovement;
    [SerializeField] private SkinnedMeshRenderer fishMesh;
    [SerializeField] private MeshCollider fishMeshCollider;

    int minSize;
    int maxSize;

    float size;

    bool carnivorous;
    bool herbivorous;
    

    [Header("Movement")]
    public float normalSpeed;
    public float hungerSpeed;

    public float hunger;
    public float hungerCop;
    Rigidbody rb;

    
    

    private void Awake()
    {
        // fishMovement = this.GetComponent<FishMovement>();

        fishMovement.moveSpeed = normalSpeed;

        fishMesh.sharedMesh = fishType.mesh;
        fishMeshCollider.sharedMesh = fishType.mesh;

        carnivorous = fishType.carnivorous;
        herbivorous = fishType.herbivorous;

        minSize = fishType.minSize;
        maxSize = fishType.maxSize;

        if(fishType.hasSpecificColor)
        {
            this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = fishType.specificColor - fishType.colorVariation * Random.ColorHSV(0f, 1f, 1f, 1f, 0f, .2f);
        }
        else
        {
            if(carnivorous)
            {
                this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, .5f, 1f, 0.5f, 1f);
            }
            else
            {
                this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 0.2f, 0.5f, 0.8f, 1f);
            }
        }

        size =  Random.Range(minSize , maxSize);
        
        this.gameObject.transform.localScale = new Vector3(0.30f,0.30f,0.30f) * size;

        hungerCop = hunger;

        rb = GetComponent<Rigidbody>();
        rb.mass = rb.mass * size;
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
        hunger -= 1 * Time.deltaTime;

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