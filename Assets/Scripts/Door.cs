using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.Instance.LevelComplete();
    }
}
