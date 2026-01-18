using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 4.5f;
    public int damage = 1;

    private Rigidbody _rb;
    private Transform _player;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) _player = p.transform;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver()) return;
        if (_player == null) return;

        Vector3 toPlayer = (_player.position - _rb.position);
        toPlayer.y = 0f;
        Vector3 dir = toPlayer.normalized;

        Vector3 next = _rb.position + dir * moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(next);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver()) return;

        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null) GameManager.Instance.DamagePlayer(damage);
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
