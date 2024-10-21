using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset;   // Offset between the player and the camera
    public float smoothSpeed = 0.125f; // Speed at which the camera follows the player

    void Start()
    {
        // Initialize the offset based on initial positions (if not set manually in Inspector)
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Desired position of the camera based on the player's position + offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the new position to the camera
        transform.position = smoothedPosition;

        // Optionally make the camera look at the player (helps for 3D platformers)
        transform.LookAt(player);
    }
}
