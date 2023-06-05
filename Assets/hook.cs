using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour
{
    //How much it exists
    [SerializeField]GameObject fisherman;
    void Start()
    {
        
    }
    private void Awake()
    {
        StartCoroutine(Dissapear());
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fish")) 
        {
        other.gameObject.GetComponent<Fish>().Die();
        }
    }
    IEnumerator Dissapear() 
    {
        yield return new WaitForSeconds(fisherman.GetComponent<Fisherman>().hookingTime);
        Destroy(gameObject);
    }
}
