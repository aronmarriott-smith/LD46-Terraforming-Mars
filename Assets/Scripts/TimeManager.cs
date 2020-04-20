using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static event Action Tick;

    private const float TICK_TIMER_MAX = .2f;
    private float tickTimer = 0;

    private void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer = 0;
            Tick?.Invoke();
        }
    }
}
