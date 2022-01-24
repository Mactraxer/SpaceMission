using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{

    [SerializeField]
    private Rigidbody _rocket;

    [SerializeField]
    private float _engineForce = 0.5f;

    public void StartLeftEngine()
    {
        _rocket.AddTorque(Vector3.back * _engineForce, ForceMode.Impulse);
    }

    public void StartRightEngine()
    {
        _rocket.AddTorque(Vector3.forward * _engineForce, ForceMode.Impulse);
    }
}
