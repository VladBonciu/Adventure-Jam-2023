using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    //How much it exists
    [SerializeField] GameObject fisherman;
    [SerializeField] Transform hookOrigin;

    [SerializeField] LineRenderer line;

    [SerializeField] public float setHeight;

    float initalYPositon;
    [SerializeField] float moveSpeed;

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
        // transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y,  initalYPositon + setHeight, Time.deltaTime * moveSpeed), transform.position.z);
        // transform.SetPositionAndRotation(transform.position - (new Vector3(0f, Mathf.Lerp(transform.position.y,  setHeight - initalYPositon, Time.deltaTime * moveSpeed) ,0f)),transform.rotation); 

        Vector3 targetPosition = new Vector3(transform.position.x, initalYPositon - setHeight, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fish")) 
        {
            Debug.Log("Hooked");

            if(other.gameObject.GetComponent<Fish>())
            {
                other.gameObject.GetComponent<Fish>().Die();
                HookedFish(other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color, other.gameObject.GetComponent<Fish>().size);
            }
            else if(other.gameObject.GetComponent<PlayerController>())
            {
                other.gameObject.GetComponent<PlayerController>().Die();
                HookedFish(other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color, 3f);
            }
            
        }
    }

    void HookedFish(Color color, float size)
    {
        GameObject caught = fleshPrefab;
        caught.transform.localScale = new Vector3(1f, 1f, 1f) * size * 2f;

        //TO BE FIXED
        caught.gameObject.GetComponent<Flesh>().color = color;
        caught.gameObject.GetComponent<Flesh>().size = size;
        caught.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        Instantiate(caught, hookOrigin);

        

        RetrieveHook(1f);
    }

    void RetrieveHook(float speed)
    {
        setHeight = 0f;
        moveSpeed = speed;
    }

    IEnumerator Dissapear() 
    {
        yield return new WaitForSeconds(fisherman.GetComponent<Fisherman>().hookingTime);
        RetrieveHook(10f);
        yield return new WaitUntil(() => transform.position.y >= 8.7f);
        Destroy(gameObject);
    }
}
