using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float heightOffset = 2f; // New variable for additional height

    void Start()
    {
        if (offset == Vector3.zero)
        {
            // Add extra height to the initial offset
            offset = transform.position - player.position;
            offset.y += heightOffset; // Adds extra height
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Modified LookAt to aim slightly above the player
        Vector3 lookAtPosition = player.position + Vector3.up * heightOffset * 0.5f;
        transform.LookAt(lookAtPosition);
    }
}