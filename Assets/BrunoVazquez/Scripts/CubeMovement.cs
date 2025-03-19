using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private Transform playerTransform;
    private float speed;

    public void SetTarget(Transform target, float moveSpeed)
    {
        playerTransform = target;
        speed = moveSpeed;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
