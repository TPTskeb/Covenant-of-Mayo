using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Drag and drop the Player here
    public Vector3 offset = new Vector3(0, 2, -10); // Adjust to keep the player visible
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (player == null) return; // Avoid errors if player is missing

        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}