using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    private RocketService _rocketService;

    [SerializeField] private GameObject _rocket;

    [SerializeField] private LaunchSettings _launchSettingsScreen;

    // Fuel UI
    [SerializeField] private Text _fuelForFirstStageText;
    [SerializeField] private Text _fuelForSecondStageText;
    [SerializeField] private Text _fuelForThirdStageText;

    // MonoBehavior
    #region MonoBehaviour methods
    void Start()
    {
        var rocketSettings = new RocketSettings();
        _rocketService = new RocketService(rocketSettings);    
    }

    void Update()
    {
        
    }
    #endregion

    // Public methods
    #region Public methods
    public void ShowLaunchSettingsScreen()
    {
        _launchSettingsScreen.Show();
        _launchSettingsScreen.WillDismis += LaunchSettingsClosed;
        print("ShowLaunchSettingsScreen");
    }

    public void ShowMissionInfoScreen()
    {

    }

    public void SwitchAutopilot()
    {
        InstanseServices.instanse.RocketService.rocketSettings.SwitchAutopilot();
    }

    public void LaunchAction()
    {
        if (InstanseServices.instanse.RocketService.rocketSettings.GetAutopilot())
        {
            var rocketAutomaticMovement = _rocket.AddComponent<RocketAutomaticMovement>();
            rocketAutomaticMovement.OnChangeFuels += UpdateFuelsText;
            rocketAutomaticMovement.Launch();
            var rigidbody = rocketAutomaticMovement.GetComponent<Rigidbody2D>();
            if ( rigidbody != null)
            {
                rocketAutomaticMovement.Rocket = rigidbody;
            }
        }
        else
        {
            var rocketManualMovement = _rocket.AddComponent<RocketManualMovement>();
        }
        
    }
    #endregion

    // Private methods
    #region Private methods
    private void UpdateFuelsText(float firstStageFuel, float secondStageFuel, float thirdStageFuel)
    {
        _fuelForFirstStageText.text = firstStageFuel.ToString();
        _fuelForSecondStageText.text = secondStageFuel.ToString();
        _fuelForThirdStageText.text = thirdStageFuel.ToString();
    }

    private void LaunchSettingsClosed()
    {
        RocketSettings settings = InstanseServices.instanse.RocketService.rocketSettings;
        var fuelForFirstStage = settings.GetFuelForStage(0);
        var fuelForSecondStage = settings.GetFuelForStage(1);
        var fuelForThirdStage = settings.GetFuelForStage(2);
        UpdateFuelsText(fuelForFirstStage, fuelForSecondStage, fuelForThirdStage);
    }
    #endregion
}
