using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public static Plant Instance;

    public static event System.Action MaxLevelReached;

    [SerializeField] private Image spriteRenderer;
    [SerializeField] private List<PlantLevel> plantLevels;

    [System.Serializable]
    public class PlantLevel
    {
        public int Level;
        public Sprite Sprite;
        public int Water;
        public int SpriteWidth;
        public int SpriteHeight;
    }

    [SerializeField] private int TicksPerWater = 5;
    private int tickCounter = 0;

    public bool SeedPlanted { get; private set; }
    public int CurrentPlantLevel { get; private set; }
    public int WaterAmount { get; private set; }
    public bool CanLevelUp => WaterAmount >= plantLevels[CurrentPlantLevel].Water;
    public string WaterLevelProgress => "" + WaterAmount + "/" + plantLevels[CurrentPlantLevel].Water;

    private void Awake()
    {
        Instance = this;

        //TimeManager.Tick += WaterOnTick;
        tickCounter = 0;

        CurrentPlantLevel = SaveData.GetInt("PLANT_CURRENT_LEVEL", 0);
        WaterAmount = SaveData.GetInt("PLANT_WATER_AMOUNT", 0);
        SeedPlanted = SaveData.GetInt("PLANT_SEED_PLANTED", 0) == 0 ? false : true;


        //spriteRenderer.sprite = plantLevels[CurrentPlantLevel].Sprite;
        spriteRenderer.sprite = plantLevels[CurrentPlantLevel].Sprite;
    }

    private void OnDestroy()
    {
        //TimeManager.Tick -= WaterOnTick;
        SaveData.SetInt("PLANT_CURRENT_LEVEL", CurrentPlantLevel);
        SaveData.SetInt("PLANT_WATER_AMOUNT", WaterAmount);
        SaveData.SetInt("PLANT_SEED_PLANTED", (SeedPlanted ? 1 : 0));
        SaveData.Save();
    }

    public void LevelUp()
    {
        //TODO: check to make sure we have enougth water to level up.
        CurrentPlantLevel = Mathf.Clamp(CurrentPlantLevel + 1, 0, plantLevels.Count -1);
        spriteRenderer.sprite = plantLevels[this.CurrentPlantLevel].Sprite;
        var rect = spriteRenderer.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2((plantLevels[this.CurrentPlantLevel].SpriteWidth) / 3, (plantLevels[this.CurrentPlantLevel].SpriteHeight) / 3);

        if (CurrentPlantLevel == plantLevels.Count - 1)
        {
            MaxLevelReached?.Invoke();
        }
    }

    public void Water()
    {
        this.WaterAmount = Mathf.Clamp(WaterAmount + 1, 0, plantLevels[this.CurrentPlantLevel].Water);
    }

    private bool CheckCanLevelUp()
    {
        return (WaterAmount >= plantLevels[CurrentPlantLevel].Water);
    }

    public bool SetSeedPlanted()
    {
        return SeedPlanted = true;
    }

    /*private void WaterOnTick()
    {
        tickCounter += 1;
        if (tickCounter >= TicksPerWater)
        {
            tickCounter = 0;
            Debug.Log("TODO: Deplete water for the plant.");
        }
    }*/
}
