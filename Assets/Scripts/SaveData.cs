using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public static SaveData Instance;

    private Dictionary<string, int> settings;

    public SaveData()
    {
        Instance = this;
        settings = new Dictionary<string, int>();

        string json = PlayerPrefs.GetString("SAVE_DATA", "");
        if (json.Length > 0)
        {
            settings = JsonUtility.FromJson<Dictionary<string, int>>(json);
        }
    }

    public static int GetInt(string Key, int defaultValue)
    {
        if (Instance == null)
            Instance = new SaveData();

        return Instance.InternalGetInt(Key, defaultValue);
    }

    public static void SetInt(string Key, int value = 0)
    {
        if (Instance == null)
            Instance = new SaveData();
        Instance.InternalSetInt(Key, value);
    }

    public static void Clear()
    {
        if (Instance == null)
            Instance = new SaveData();

        Instance.InternalClear();
    }

    public static void Save()
    {
        if (Instance == null)
            Instance = new SaveData();

        Instance.InternalSave();
    }

    private void InternalSave()
    {
        PlayerPrefs.SetString("SAVE_DATA", JsonUtility.ToJson(settings));
    }


    private void InternalClear()
    {
        settings = new Dictionary<string, int>();
    }

    private int InternalGetInt(string Key, int defaultValue)
    {
        bool found = settings.TryGetValue(Key, out int returnValue);

        if (found)
        {
            return returnValue;
        }

        return defaultValue;
    }

    private void InternalSetInt(string Key, int value)
    {
        // check if key exists before updating the value.
        if (settings.ContainsKey(Key))
        {
            settings.Remove(Key);
        }
        settings.Add(Key, value);
    }
}
