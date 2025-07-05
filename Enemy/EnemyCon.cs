using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Ensure kinematic
    }

    void Update()
    {
        // Calculate movement direction
        Vector2 direction = (player.transform.position - transform.position);
        moveDirection = direction.normalized;

        // Rotate toward player
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    void FixedUpdate()
    {
        // Move toward player using MovePosition
        Vector2 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}


