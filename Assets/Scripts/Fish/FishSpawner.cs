using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    [SerializeField] float spawnCount;

    [SerializeField] private FishType[] fishTypes;
    
    void Start()
    {
        for (int i = 0; i< spawnCount; i++) {

            GameObject fish = fishPrefab;
            FishType type = fishTypes[Random.Range(0, fishTypes.Length)];
            fish.GetComponent<Fish>().fishType = type;

            Instantiate(fish, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 10), Quaternion.identity);
            
            Debug.Log("Fish spawned");
        }
    }

}
