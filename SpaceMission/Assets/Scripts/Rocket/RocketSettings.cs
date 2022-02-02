using System.Collections.Generic;

public class RocketSettings { 

    private int _totalFuel;
    private bool _isAutoPilot;
    private Dictionary<int, int> _fuelForStage;
    private float _engineForce;
    private float _torqueEngineForce;

    public RocketSettings()
    {
        _totalFuel = 0;
        _isAutoPilot = true;
        _fuelForStage = new Dictionary<int, int>();
        _engineForce = 10000f;
        _torqueEngineForce = 5000f;
    }

    public void SetTotalFuel(int fuel)
    {
        _totalFuel = fuel;
    }

    public int GetTotalFuel()
    {
        return _totalFuel;
    }

    public void SetStageCount(int count)
    {
        _fuelForStage.Clear();
        for (int i = 0; i < count; i++)
        {
            _fuelForStage.Add(i,0);
        }
    }

    public void SetFuelForStage(int fuel, int stage)
    {
        _fuelForStage[stage] = fuel;
    }

    public int GetFuelForStage(int stage)
    {
        return _fuelForStage[stage];
    }

    public void SwitchAutopilot()
    {
        _isAutoPilot = !_isAutoPilot;
    }

    public bool GetAutopilot()
    {
        return _isAutoPilot;
    }

    public float GetEngineForce()
    {
        return _engineForce;
    }

    public float GetTorqueEngineForce()
    {
        return _torqueEngineForce;
    }
}
