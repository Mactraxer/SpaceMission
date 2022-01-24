using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LaunchRocket : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Rocket;

    [SerializeField]
    private Text _fuelInput;
    private float _fuel;

    [SerializeField]
    private float _engineForce = 400f;


    public void Launch()
    {
        ParseFuelInput();
        StartCoroutine(BurnFuel());
    }

    private void ParseFuelInput()
    {
        _fuel = float.Parse(_fuelInput.text);
        
    }

    IEnumerator BurnFuel()
    {
        
        while (_fuel > 0)
        {
            _fuel--;
            Debug.Log("Fuel burn");
            ApplyEngineForce();
            yield return new WaitForSeconds(.1f);
        }
        
    }

    private void ApplyEngineForce()
    {
        Debug.Log("Engine forced");
        Rocket.AddRelativeForce(Vector3.up * _engineForce, ForceMode.Force);
    }
}
