using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fish;
    public float spawnCount;
    
    void Start()
    {
        for (int i = 0; i< spawnCount; i++) {
            Instantiate(fish, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 10), Quaternion.identity);
            Debug.Log("Fish spawned");
        }
    }

}
