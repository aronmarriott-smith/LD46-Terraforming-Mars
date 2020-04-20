using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<Transform> Loaded;

    private int TotalWater = 0;
    private int MAX_WATER = 30;

    private void Awake()
    {
        TotalWater = 0;
        Loaded?.Invoke(transform);
    }

    public void AddWater(int amount = 0)
    {
        this.TotalWater = Mathf.Clamp(TotalWater + amount, 0, MAX_WATER);
    }

    public void DecreaseWater(int amount = 0)
    {
        this.TotalWater = Mathf.Clamp(TotalWater - amount, 0, MAX_WATER);
    }
}
