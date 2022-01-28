using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D _rocket;

    [SerializeField]
    private float _engineForce = 0.1f;
    private float _engineAccumulativeForce = 0f;// acceleration

    [SerializeField]
    private float _torqueEngineForce = 0.1f;
    private float _torqueAccumulativeForce = 0f;// acceleration

    [SerializeField]
    private Text _fuelText;
    private float _fuel;

    [SerializeField]
    private Text _speedText;
    private float _speed;
    private Vector3 previousPosition;

    [SerializeField]
    private ParticleSystem _backEnginePartricle;
    [SerializeField]
    private ParticleSystem _topEngineParticle;
    [SerializeField]
    private ParticleSystem _leftEngineParticle;
    [SerializeField]
    private ParticleSystem _rightEngineParticle;

    private List<Coroutine> _coroutineEngineList;

    private void Start()
    {
        _fuel = 1000f;
        UpdateFuelText();
        previousPosition = gameObject.transform.position;
        StartCoroutine(Speedometr());
        UpdateSpeedText();
        _coroutineEngineList = new List<Coroutine>();
    }

    private void UpdateFuelText()
    {
        _fuelText.text = _fuel.ToString();
    }

    private void UpdateSpeedText()
    {
        _speedText.text = _speed.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var newCoroutine = StartCoroutine(AddForceBackEngine());
            _coroutineEngineList.Add(newCoroutine);
        } else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            StopEngine();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var newCoroutine = StartCoroutine(AddForceFrontEngine());
            _coroutineEngineList.Add(newCoroutine);
        } else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            StopEngine();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var newCoroutine = StartCoroutine(AddForceLeftEngine());
            _coroutineEngineList.Add(newCoroutine);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            StopEngine();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var newCoroutine = StartCoroutine(AddForceRightEngine());
            _coroutineEngineList.Add(newCoroutine);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            StopEngine();
        }
    }

    private void StopEngine()
    {
        foreach (var item in _coroutineEngineList)
        {
            StopCoroutine(item);
        }
        
        _coroutineEngineList = new List<Coroutine>();
        _engineAccumulativeForce = 0f;
        _torqueAccumulativeForce = 0f;
        _backEnginePartricle.Stop();
        _topEngineParticle.Stop();
        _leftEngineParticle.Stop();
        _rightEngineParticle.Stop();

    }

    private IEnumerator Speedometr()
    {
        while (true)
        {
            _speed = Vector3.Distance(previousPosition, _rocket.transform.position) / Time.deltaTime;
            
            _speed = Mathf.Round(_speed);
            UpdateSpeedText();
            previousPosition = gameObject.transform.position;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator AddForceLeftEngine()
    {
        _leftEngineParticle.Play();
        while (true)
        {
            _torqueAccumulativeForce += _torqueEngineForce;
            _rocket.AddTorque(-1 * _torqueAccumulativeForce, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
            BurnFuel();
        }
    }

    private IEnumerator AddForceRightEngine()
    {
        _rightEngineParticle.Play();
        while (true)
        {
            _torqueAccumulativeForce += _torqueEngineForce;
            _rocket.AddTorque(1 * _torqueAccumulativeForce, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
            BurnFuel();
        }
    }

    private IEnumerator AddForceFrontEngine()
    {
        _topEngineParticle.Play();
        while (true)
        {
            _engineAccumulativeForce += _engineForce;
            _rocket.AddRelativeForce(Vector2.down * _engineAccumulativeForce, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
            BurnFuel();
        }
    }

    private IEnumerator AddForceBackEngine()
    {
        _backEnginePartricle.Play();
        while (true)
        {
            _engineAccumulativeForce += _engineForce;
            _rocket.AddRelativeForce(Vector2.up * _engineAccumulativeForce, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
            BurnFuel();
        }
        
    }

    private void BurnFuel()
    {
        _fuel--;
        UpdateFuelText();
    }
}
