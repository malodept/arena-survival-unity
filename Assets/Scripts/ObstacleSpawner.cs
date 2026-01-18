using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;

    [Header("Spawn")]
    public float spawnInterval = 1.0f;
    public float spawnRadius = 14f;
    public float minDistanceFromPlayer = 6f;

    [Header("Difficulty (optional)")]
    public float intervalDecay = 0.02f;   // interval decreases over time
    public float minInterval = 0.35f;

    private float _timer;

    private void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver()) return;
        if (obstaclePrefab == null || player == null) return;

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            SpawnOne();
            _timer = spawnInterval;

            // Optional: slowly ramp difficulty
            spawnInterval = Mathf.Max(minInterval, spawnInterval - intervalDecay);
        }
    }

    private void SpawnOne()
    {
        Vector2 r = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 pos = player.position + new Vector3(r.x, 0.5f, r.y);

        // Ensure not too close
        if (Vector3.Distance(pos, player.position) < minDistanceFromPlayer)
        {
            pos = player.position + (pos - player.position).normalized * minDistanceFromPlayer;
        }

        Instantiate(obstaclePrefab, pos, Quaternion.identity);
    }
}
