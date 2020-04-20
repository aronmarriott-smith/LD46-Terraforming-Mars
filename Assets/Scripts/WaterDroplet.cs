using System;
using UnityEngine;

public class WaterDroplet : MonoBehaviour
{
    public static event Action DropletCollected;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.Instance.AddDroplet();
        Destroy(gameObject);
    }
}
