using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the object moves

    void Update()
    {
        // Check if the D key is pressed
        if (Input.GetKey(KeyCode.D))
        {
            // Move the GameObject along the X-axis
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }
}
