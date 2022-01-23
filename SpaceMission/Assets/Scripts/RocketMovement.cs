using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{

    [SerializeField]
    private Rigidbody _rocket;

    private float force = 0.5f;

    public void StartLeftEngine()
    {
        _rocket.AddTorque(Vector3.back * force, ForceMode.Impulse);
    }

    public void StartRightEngine()
    {
        _rocket.AddTorque(Vector3.forward * force, ForceMode.Impulse);
    }
}
