using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish: MonoBehaviour {

    [Header("General")]
    public FishType fishType;
    [SerializeField] private FishMovement fishMovement;
    [SerializeField] private SkinnedMeshRenderer fishMesh;
    [SerializeField] private MeshCollider fishMeshCollider;
    [SerializeField] Transform meshObject;

    int minSize;
    int maxSize;
    public float size;

    bool carnivorous;
    bool herbivorous;
    

    [Header("Movement")]
    public float normalSpeed;
    public float hungerSpeed;

    public float hunger;
    public float hungerCop;

    bool isHungry;
    Rigidbody rb;

    

     private float detectionRange;


    private void Awake()
    {
        // fishMovement = this.GetComponent<FishMovement>();

        isHungry = false;

        fishMovement.moveSpeed = normalSpeed;

        fishMesh.sharedMesh = fishType.mesh;
        fishMeshCollider.sharedMesh = fishType.mesh;

        carnivorous = fishType.carnivorous;
        herbivorous = fishType.herbivorous;

        minSize = fishType.minSize;
        maxSize = fishType.maxSize;

        detectionRange = fishType.detectionRange;

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

        if(hunger < 50f)
        {
            isHungry = true;
            SearchForFood();
        }
        else
        {
            isHungry = false;
        }

        if (hunger <= 0) 
        {
            Die();
        }
        
    }
    void Eat(float amount)
    {
        hunger += amount * 10f;
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
        Destroy(this.gameObject);
    }

    void SearchForFood()
    {               
        RaycastHit hit;

        Debug.DrawRay(meshObject.transform.position, -meshObject.transform.forward * detectionRange, Color.red, 1f);
        Debug.DrawRay(meshObject.transform.position, (-meshObject.transform.forward + Vector3.up) * detectionRange, Color.white, 1f);
        Debug.DrawRay(meshObject.transform.position, (-meshObject.transform.forward+ Vector3.down)* detectionRange, Color.white, 1f);
                    
        if(  ( Physics.Raycast(meshObject.transform.position, -meshObject.transform.forward, out hit, detectionRange)) || ( Physics.Raycast( meshObject.transform.position, -meshObject.transform.forward + Vector3.up, out hit, detectionRange )) || ( Physics.Raycast(meshObject.transform.position, -meshObject.transform.forward + Vector3.down, out hit, detectionRange ))  )
        {
            if((hit.collider.gameObject.layer == LayerMask.NameToLayer("Fish")) && carnivorous)
            {  
                fishMovement.moveSpeed = fishType.hungerSpeed;
                fishMovement.TartgetObject(hit.point);
            }
            else if((hit.collider.gameObject.layer == LayerMask.NameToLayer("Vegetation")) && herbivorous)
            {
                fishMovement.moveSpeed = fishType.hungerSpeed;
                fishMovement.TartgetObject(hit.point);
            }   
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Fish") && carnivorous && isHungry)
        {
            if(collision.gameObject.GetComponent<Fish>())
            {
                Eat(collision.gameObject.GetComponent<Fish>().size);
                Debug.Log("Ate fish of " + collision.gameObject.GetComponent<Fish>().size);
            }
            else
            {
                //Player
                Eat(3f);
            }
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Vegetation") && herbivorous && isHungry)
        {
            Eat(5f);
            Destroy(collision.gameObject);
        }
    }
}
