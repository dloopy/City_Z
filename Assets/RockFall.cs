using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : MonoBehaviour
{
    public Rigidbody rocks;
    public Transform instantiatePosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(rocks, instantiatePosition.position, instantiatePosition.rotation);
        }
    }
}
