using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] float time;

    void Awake()
    {
        StartCoroutine(Countdown(time));
    }

    IEnumerator Countdown(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
