using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : MonoBehaviour
{
    [SerializeField] private int DamageAmount = 1;

    private PlayerHealth playerHealth;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (playerHealth != null)
            playerHealth.DecreaseHealth(DamageAmount);
        Destroy(gameObject);
    }

}
