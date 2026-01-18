using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float maxDistance = 100f;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver()) return;

        if (Input.GetMouseButtonDown(0))
        {
            ShootRay();
        }
    }

    void ShootRay()
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            Obstacle obstacle = hit.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                obstacle.Die();
                if (GameManager.Instance != null)
                    GameManager.Instance.AddScore(1);
            }
        }
    }
}
