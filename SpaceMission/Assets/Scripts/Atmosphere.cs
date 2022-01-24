using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atmosphere : MonoBehaviour
{
    [SerializeField] float _gravityForce;
    private Rigidbody _target;
    private float _gravityMulticator;

    void OnTriggerEnter(Collider other)
    {
        var rigidBody = other.gameObject.GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            _target = rigidBody;
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        var rigidBody = other.gameObject.GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            _target = null;
        }
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            
            _target.AddForceAtPosition((gameObject.transform.position - _target.position) * _gravityForce, gameObject.transform.position, ForceMode.Force);
        }
        
    }

}
