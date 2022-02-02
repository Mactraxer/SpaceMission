using UnityEngine.UI;
using UnityEngine;

public class LaunchSettings : MonoBehaviour
{

    [SerializeField] private InputField _fuelFirstStageInputField;
    [SerializeField] private InputField _fuelSecondStageInputField;
    [SerializeField] private InputField _fuelThirdStageInputField;

    [SerializeField] private Text _totalFuel;

    private RocketSettings _rocketSettings = InstanseServices.instanse.RocketService.rocketSettings;

    public event System.Action WillDismis;



    // MonoBehaviour methods
    #region MonoBehaviour
    private void Start()
    {
        
    }

    
    private void Update()
    {
        
    }
    #endregion

    // Private methods
    #region Private methods
    private void UpdateFuelText()
    {
        _totalFuel.text = _rocketSettings.GetTotalFuel().ToString();
    }
    #endregion

    // Public methods
    #region Public methods
    public void ApplySettingsAction()
    {
        var firstStageFuel = int.Parse(_fuelFirstStageInputField.text);
        var secondStageFuel = int.Parse(_fuelSecondStageInputField.text);
        var thirdStageFuel = int.Parse(_fuelThirdStageInputField.text);


        _rocketSettings.SetStageCount(3);
        _rocketSettings.SetFuelForStage(firstStageFuel, 0);
        _rocketSettings.SetFuelForStage(secondStageFuel, 1);
        _rocketSettings.SetFuelForStage(thirdStageFuel, 2);
        CloseAction();
    }

    public void CloseAction()
    {
        WillDismis.Invoke();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        UpdateFuelText();
        gameObject.SetActive(true);
    }
    #endregion

}
