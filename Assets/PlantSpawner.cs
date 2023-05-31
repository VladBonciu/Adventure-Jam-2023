using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    public GameObject plant;
    [SerializeField] float spawnCount;

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(plant, new Vector3(Random.Range(-20, 20),10,10) , Quaternion.identity);
            Debug.Log("Plant spawned");
        }
    }
}
