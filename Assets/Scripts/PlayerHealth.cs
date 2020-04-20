using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<int> HealthChanged;

    public static PlayerHealth Instance;

    public int Health { get; private set; }
    private int MAX_HEALTH = 3;
    
    private void Awake()
    {
        Instance = this;
        Health = MAX_HEALTH;
        HealthChanged?.Invoke(Health);
    }

    public void ResetHealth()
    {
        this.Health = MAX_HEALTH;
    }

    public void AddHealth(int amount = 0)
    {
        this.Health = Mathf.Clamp(Health + amount, 0, MAX_HEALTH);
        HealthChanged?.Invoke(this.Health);
    }

    public void DecreaseHealth(int amount = 0)
    {
        this.Health = Mathf.Clamp(Health - amount, 0, MAX_HEALTH);
        HealthChanged?.Invoke(this.Health);
    }
}
