using UnityEngine;

public class LaunchRocket : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Rocket;

    private float force = 400f;


    public void Launch()
    {
        Rocket.AddRelativeForce(Vector3.up * force, ForceMode.Force);
    }
}
