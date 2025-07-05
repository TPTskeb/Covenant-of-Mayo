using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float collisionOffset = 0.01f;
    public ContactFilter2D movementFilter;

    private Vector2 moveInput;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        // Get input from keyboard or other input system
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        // Optional: Normalize input to prevent faster diagonal movement
        moveInput = moveInput.normalized;
    }

    public void FixedUpdate()
    {
        bool success = MovePlayer(moveInput);

        if (!success)
        {
            success = MovePlayer(new Vector2(moveInput.x, 0));

            if (!success)
            {
                success = MovePlayer(new Vector2(0, moveInput.y));
            }
        }
    }

    public bool MovePlayer(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        else
        {
            // Optional: log collisions
            foreach (RaycastHit2D hit in castCollisions)
            {
                Debug.Log(hit.ToString());
            }
            return false;
        }
    }
}
