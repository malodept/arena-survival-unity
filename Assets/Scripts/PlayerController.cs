using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Rotate to movement direction (optional, makes it nicer)
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (input.sqrMagnitude > 0.001f)
        {
            Vector3 dir = input.normalized;
            transform.forward = Vector3.Slerp(transform.forward, dir, Time.deltaTime * 12f);
        }
    }

    private void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Vector3 velocity = input.normalized * moveSpeed;

        Vector3 nextPos = _rb.position + velocity * Time.fixedDeltaTime;
        _rb.MovePosition(nextPos);
    }
}
