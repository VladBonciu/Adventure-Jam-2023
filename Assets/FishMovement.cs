using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float movementTime;
    bool moving;
    float movex;
    float movey;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
     StartCoroutine(Move());
     
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) 
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(movex,movey, 10), Time.deltaTime * 0.1f);
        }
    }
    IEnumerator Move() 
    {
        moving = true;
        yield return new WaitForSeconds(movementTime);
        moving = false;
        movex = Random.Range(-20, 20);
        movey = Random.Range(-10, 10);
        movementTime = Random.Range(1, 7);
        yield return new WaitForSeconds(movementTime);
        
        StartCoroutine(Move());
    }
}
