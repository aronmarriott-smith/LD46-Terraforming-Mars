using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkshopUI : MonoBehaviour
{
    public static event System.Action OnWaterPlant;

    [SerializeField] private TextMeshProUGUI plantLevelText;
    [Header("Buttons")]
    [SerializeField] private Button plantSeedButton;
    [SerializeField] private Button waterPlantButton;
    [SerializeField] private Button levelUpPlantButton;
    [SerializeField] private Button collectWaterButton;

    private Plant plant;

    private void Awake()
    {
        if (plant == null)
            plant = GameObject.FindObjectOfType<Plant>();

        plantSeedButton.interactable = CanPlantSeed();
    }

    private void Update()
    {
        waterPlantButton.interactable = CanWater();
        collectWaterButton.interactable = CanCollectWater();
        levelUpPlantButton.interactable = CanLevelUp();
        plantLevelText.text = plant.WaterLevelProgress;
    }

    public void CollectWater()
    {
        GameManager.Instance.LoadLevel();
    }

    public void PlantSeed()
    {
        plant.SetSeedPlanted();
        plantSeedButton.interactable = false;
        collectWaterButton.interactable = true;
    }

    public void WaterPlant()
    {
        if (GameManager.Instance.WaterDropletTotal > 0)
        {
            OnWaterPlant?.Invoke();
            plant.Water();
        }
    }

    public void LevelUpPlant()
    {
        if (plant.CanLevelUp)
        {
            plant.LevelUp();
        }
    }

    private bool CanPlantSeed()
    {
        return (false == plant.SeedPlanted);
    }

    private bool CanWater()
    {
        return (plant.SeedPlanted && GameManager.Instance.WaterDropletTotal > 0);
    }

    private bool CanLevelUp()
    {
        return (plant.CanLevelUp);
    }

    private bool CanCollectWater()
    {
        return (plant.SeedPlanted && GameManager.Instance.GetTimesWatered() > 0);
    }
}