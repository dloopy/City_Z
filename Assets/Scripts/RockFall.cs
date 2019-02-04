using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : MonoBehaviour
{
    public Rigidbody[] rocks;
    public Transform instantiatePosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < rocks.Length; i++)
            {

                Instantiate(rocks[i], instantiatePosition.position, instantiatePosition.rotation);
            }
        }
    }
}
