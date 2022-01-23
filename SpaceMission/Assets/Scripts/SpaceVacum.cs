using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceVacum : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        var rigidBody = other.gameObject.GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            rigidBody.useGravity = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        var rigidBody = other.gameObject.GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            rigidBody.useGravity = true;
        }
    }

}
