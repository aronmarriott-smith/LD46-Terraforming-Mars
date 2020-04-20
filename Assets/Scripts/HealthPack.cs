using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int HealthAmount = 1;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (playerHealth != null)
            playerHealth.AddHealth(HealthAmount);
        Destroy(gameObject);
    }
}
