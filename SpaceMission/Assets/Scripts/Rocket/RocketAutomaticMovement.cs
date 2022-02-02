using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RocketAutomaticMovement : MonoBehaviour
{
    
    public Rigidbody2D Rocket;

    [SerializeField]
    private float _engineForce = 0.1f;
    private float _engineAccumulativeForce = 0f;// acceleration

    [SerializeField]
    private float _torqueEngineForce = 0.1f;
    private float _torqueAccumulativeForce = 0f;// acceleration

    // Fuel
    private float _fuelForFirstStage;
    private float _fuelForSecondStage;
    private float _fuelForThirdStage;

    // Speed
    [SerializeField]
    private Text _speedText;//TODO remove to UI layer
    private float _speed;

    private Vector3 previousPosition;

    // Engines particle
    [SerializeField]
    private ParticleSystem _backEnginePartricle;
    [SerializeField]
    private ParticleSystem _topEngineParticle;
    [SerializeField]
    private ParticleSystem _leftEngineParticle;
    [SerializeField]
    private ParticleSystem _rightEngineParticle;

    //Coroutine
    private List<Coroutine> _coroutineEngineList;

    // Stages
    private int _currentStage = 1;

    // Events
    public delegate void ChangeFuels(float firstStage, float secondStage, float thirdStage);
    public event ChangeFuels OnChangeFuels;

    private void Awake()
    {
        ApplyRocketSettings();
        previousPosition = gameObject.transform.position;
        StartCoroutine(Speedometr());
        //UpdateSpeedText();
        _coroutineEngineList = new List<Coroutine>();
    }

    private void UpdateSpeedText()
    {
        _speedText.text = _speed.ToString();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    var newCoroutine = StartCoroutine(AddForceBackEngine());
        //    _coroutineEngineList.Add(newCoroutine);
        //}
        //else if (Input.GetKeyUp(KeyCode.UpArrow))
        //{
        //    StopEngine();
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    var newCoroutine = StartCoroutine(AddForceFrontEngine());
        //    _coroutineEngineList.Add(newCoroutine);
        //}
        //else if (Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    StopEngine();
        //}

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    var newCoroutine = StartCoroutine(AddForceLeftEngine());
        //    _coroutineEngineList.Add(newCoroutine);
        //}
        //else if (Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    StopEngine();
        //}

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    var newCoroutine = StartCoroutine(AddForceRightEngine());
        //    _coroutineEngineList.Add(newCoroutine);
        //}
        //else if (Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    StopEngine();
        //}
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
        //_backEnginePartricle.Stop();
        //_topEngineParticle.Stop();
        //_leftEngineParticle.Stop();
        //_rightEngineParticle.Stop();

    }

    private IEnumerator Speedometr()
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        while (true)
        {
            _speed = Vector3.Distance(previousPosition, Rocket.transform.position) / Time.deltaTime;

            _speed = Mathf.Round(_speed);
            //UpdateSpeedText();
            previousPosition = gameObject.transform.position;
            yield return waitForFixedUpdate;
        }
    }

    private IEnumerator AddForceLeftEngine()
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        //_leftEngineParticle.Play();
        while (true)
        {
            _torqueAccumulativeForce += _torqueEngineForce;
            Rocket.AddTorque(-1 * _torqueAccumulativeForce, ForceMode2D.Force);

            yield return waitForFixedUpdate;
            SimulateEngineWork();
        }
    }

    private IEnumerator AddForceRightEngine()
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        //_rightEngineParticle.Play();
        while (true)
        {
            _torqueAccumulativeForce += _torqueEngineForce;
            Rocket.AddTorque(1 * _torqueAccumulativeForce, ForceMode2D.Force);

            yield return waitForFixedUpdate;
            SimulateEngineWork();
        }
    }

    private IEnumerator AddForceFrontEngine()
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        //_topEngineParticle.Play();
        while (true)
        {
            _engineAccumulativeForce += _engineForce;
            Rocket.AddRelativeForce(Vector2.down * _engineAccumulativeForce, ForceMode2D.Force);

            yield return waitForFixedUpdate;
            SimulateEngineWork();
        }
    }

    private IEnumerator AddForceBackEngine()
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        //_backEnginePartricle.Play();
        while (true)
        {
            _engineAccumulativeForce += _engineForce;
            Rocket.AddRelativeForce(Vector2.up * _engineAccumulativeForce, ForceMode2D.Force);
            yield return waitForFixedUpdate;
            SimulateEngineWork();
        }
    }

    private void ApplyRocketSettings()
    {
        RocketSettings settings = InstanseServices.instanse.RocketService.rocketSettings;
        _fuelForFirstStage = settings.GetFuelForStage(0);
        _fuelForSecondStage = settings.GetFuelForStage(1);
        _fuelForThirdStage = settings.GetFuelForStage(2);

        _engineForce = settings.GetEngineForce();
        _torqueEngineForce = settings.GetTorqueEngineForce();
    }

    // Public methods
    #region Public methods
    public void Launch()
    {
        var newCoroutine = StartCoroutine(AddForceFrontEngine());
        _coroutineEngineList.Add(newCoroutine);
    }
    #endregion

    // Engine work
    #region Engine simulation methods
    private void SimulateEngineWork()
    {
        if (HaveFuel(_currentStage))
        {
            BurnFuel(_currentStage);
        }
        else
        {
            SwithStage();
            if (_currentStage > 3)
            {
                StopEngine();
            }
            else
            {
                BurnFuel(_currentStage);
            }
        }
    }

    private bool HaveFuel(int forStage)
    {
        switch (forStage)
        {
            case 1:
                return _fuelForFirstStage > 0;
            case 2:
                return _fuelForSecondStage > 0;
            case 3:
                return _fuelForThirdStage > 0;
            default:
                Debug.Log("Over stage count", this);
                return false;
        }
    }

    private void BurnFuel(int forStage)
    {
        switch(forStage)
        {
            case 1:
                _fuelForFirstStage--;
                break;
            case 2:
                _fuelForSecondStage--;
                break;
            case 3:
                _fuelForThirdStage--;
                break;
            default:
                Debug.Log("Over stage count", this);
                break;
        }

        OnChangeFuels(_fuelForFirstStage, _fuelForSecondStage, _fuelForThirdStage);
    }

    private void SwithStage()
    {
        _currentStage++;
    }
    #endregion
}
