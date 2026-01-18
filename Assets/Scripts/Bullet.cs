using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    
        
    {
        // Ignore player
        if (other.CompareTag("Player")) return;

        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.Die();
            if (GameManager.Instance != null) GameManager.Instance.AddScore(1);
        }

        Destroy(gameObject);
    }
    
}
