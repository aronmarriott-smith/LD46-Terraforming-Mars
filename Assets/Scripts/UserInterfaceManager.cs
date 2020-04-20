using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject missionFailedMenu;
    [SerializeField] private GameObject gameWonMenu;
    [Header("Buttons")]
    [SerializeField] private Button ContinueGameButton;
    [Header("HUD")]
    [SerializeField] private GameObject HUDCanvas;
    [SerializeField] private TextMeshProUGUI WaterText;
    [SerializeField] private TextMeshProUGUI HealthText;

    private void Awake()
    {
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
        missionFailedMenu.SetActive(false);
        HUDCanvas.SetActive(false);
        gameWonMenu.SetActive(false);
        ContinueGameButton.interactable = CanContineGame();

        PlayerHealth.HealthChanged += HandleHealthChange;
        GameManager.WaterDropletTotalChanged += UpdateWaterText;
        Plant.MaxLevelReached += HandleGameWon;
    }

    private void OnDestroy()
    {
        PlayerHealth.HealthChanged -= HandleHealthChange;
        GameManager.WaterDropletTotalChanged -= UpdateWaterText;
        Plant.MaxLevelReached -= HandleGameWon;
    }

    private void HandleGameWon()
    {
        GameManager.Instance.Pause();
        GameManager.Instance.GameWon();
        gameWonMenu.SetActive(true);
    }

    private void HandleHealthChange(int Health)
    {
        if (Health == 0)
        {
            GameManager.Instance.Pause();
            missionFailedMenu.SetActive(true);
        }
        UpdateHealthText(Health);
    }

    public void NewGame()
    {
        MainMenu.SetActive(false);
        HUDCanvas.SetActive(true);
        gameWonMenu.SetActive(false);
        GameManager.Instance.NewGame();
    }

    public void ContinueGame()
    {
        MainMenu.SetActive(false);
        HUDCanvas.SetActive(true);
        GameManager.Instance.ContinueGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        GameManager.Instance.Resume();
        PauseMenu.SetActive(false);
        HUDCanvas.SetActive(true);
    }

    public void BackToWorkshop()
    {
        GameManager.Instance.BackToWorkshop();
        missionFailedMenu.SetActive(false);
    }

    public void RetryMission()
    {
        GameManager.Instance.RetryMission();
        missionFailedMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.Instance.GetCurrentState() == GameManager.State.PLAYING)
            {
                GameManager.Instance.Pause();
                PauseMenu.SetActive(true);
                HUDCanvas.SetActive(false);
            } else {
                GameManager.Instance.Resume();
                PauseMenu.SetActive(false);
                HUDCanvas.SetActive(true);
            }
        }
    }

    private void UpdateHealthText(int Health)
    {
        HealthText.text = "" + Health;
    }

    private void UpdateWaterText(int Water)
    {
        WaterText.text = "" + Water;
    }

    private bool CanContineGame()
    {
        return (SaveData.GetInt("PLANT_SEED_PLANTED", 0) == 1);
    }
}
