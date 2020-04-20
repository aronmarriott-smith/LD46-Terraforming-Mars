using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action<int> WaterDropletTotalChanged;

    [SerializeField] private string WorkshopSceneName;
    [SerializeField] private string LevelSceneName;

    public int WaterDropletTotal { get; private set; }
    private int startingWater = 10;
    
    #region Plant Stuff
    //private bool seedPlanted = false;
    private int timesWatered = 0;
    #endregion

    public enum State
    {
        PLAYING,
        PAUSED,
    }
    private State currentState;

    private void Awake()
    {
        Instance = this;
        timesWatered = 0;

        WaterDropletTotal = 0;
        WaterDropletTotal = SaveData.GetInt("WATER_DROPLETS_TOTAL", startingWater);
        WaterDropletTotalChanged?.Invoke(WaterDropletTotal);
        //Debug.Log(SaveData.GetInt("WATER_DROPLETS_TOTAL"));
        //Debug.Log(WaterDropletTotal);

        WaterDroplet.DropletCollected += AddDroplet;
        WorkshopUI.OnWaterPlant += HandleWaterPlant;
    }

    private void OnDestroy()
    {
        WaterDroplet.DropletCollected -= AddDroplet;
        WorkshopUI.OnWaterPlant -= HandleWaterPlant;
    }

    public void NewGame()
    {
        ClearSavedData();
        LoadWorkshop();
        currentState = State.PLAYING;
    }

    public void ContinueGame()
    {
        LoadWorkshop();
        currentState = State.PLAYING;
    }

    public void BackToWorkshop()
    {
        LoadWorkshop();
        Resume();
    }


    public void RetryMission()
    {
        if (SceneManager.GetSceneByName(LevelSceneName).isLoaded)
            SceneManager.UnloadSceneAsync(LevelSceneName);
        SceneManager.LoadScene(LevelSceneName, LoadSceneMode.Additive);

        Resume();
    }

    public State GetCurrentState()
    {
        return this.currentState;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        currentState = State.PAUSED;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        currentState = State.PLAYING;
    }

    public void GameWon()
    {
        if (SceneManager.GetSceneByName(WorkshopSceneName).isLoaded)
            SceneManager.UnloadSceneAsync(WorkshopSceneName);
    }

    private void LoadWorkshop()
    {
        if (false == SceneManager.GetSceneByName(WorkshopSceneName).isLoaded)
            SceneManager.LoadScene(WorkshopSceneName, LoadSceneMode.Additive);
        if (SceneManager.GetSceneByName(LevelSceneName).isLoaded)
            SceneManager.UnloadSceneAsync(LevelSceneName);
    }

    public void LoadLevel()
    {
        if (false == SceneManager.GetSceneByName(LevelSceneName).isLoaded)
            SceneManager.LoadScene(LevelSceneName, LoadSceneMode.Additive);
        if (SceneManager.GetSceneByName(WorkshopSceneName).isLoaded)
            SceneManager.UnloadSceneAsync(WorkshopSceneName);
    }

    public void AddDroplet()
    {
        WaterDropletTotal += 1;
        WaterDropletTotalChanged?.Invoke(this.WaterDropletTotal);
    }

    public void LevelComplete()
    {
        LoadWorkshop();
    }

    public void HandleWaterPlant()
    {
        timesWatered += 1;
        WaterDropletTotal -= 1;
        WaterDropletTotalChanged?.Invoke(WaterDropletTotal);
    }

    public int GetTimesWatered()
    {
        return timesWatered;
    }

    private void ClearSavedData()
    {
        SaveData.Clear();
    }

    private void OnApplicationQuit()
    {
        SaveData.SetInt("WATER_DROPLETS_TOTAL", WaterDropletTotal);

        SaveData.Save();
    }
}
