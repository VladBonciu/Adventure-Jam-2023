using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    //How much it exists
    [SerializeField] GameObject fisherman;

    [SerializeField] LineRenderer line;

    [SerializeField] public float setHeight;

    float initalYPositon;

    [SerializeField] GameObject fleshPrefab;

    private void Awake()
    {
        StartCoroutine(Dissapear());
        initalYPositon = transform.position.y;
        line.SetPosition(1, new Vector3(line.gameObject.transform.position.x, fisherman.transform.position.y, line.gameObject.transform.position.z));
    }

    void Update()
    {
        line.SetPosition(0, new Vector3(line.gameObject.transform.position.x, line.gameObject.transform.position.y, line.gameObject.transform.position.z));
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y,  initalYPositon + setHeight, Time.deltaTime), transform.position.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fish")) 
        {
            Debug.Log("Hooked");

            if(other.gameObject.GetComponent<Fish>())
            {
                other.gameObject.GetComponent<Fish>().Die();
                HookedFish();
            }
            else if(other.gameObject.GetComponent<PlayerController>())
            {
                other.gameObject.GetComponent<PlayerController>().Die();
                HookedFish();
            }
            
        }
    }

    void HookedFish()
    {
        GameObject flesh = fleshPrefab;
        flesh.transform.localScale = new Vector3(1f, 1f, 1f) * 3f * 2f;

        //TO BE FIXED
        flesh.gameObject.GetComponent<Flesh>().color = Color.white;
        flesh.gameObject.GetComponent<Flesh>().size = 3f;
        Instantiate(flesh, this.transform.position, transform.rotation);

        setHeight = 0;
    }

    IEnumerator Dissapear() 
    {
        yield return new WaitForSeconds(fisherman.GetComponent<Fisherman>().hookingTime);
        Destroy(gameObject);
    }
}
