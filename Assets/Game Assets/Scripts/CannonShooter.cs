using System.Collections;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    public GameObject ballPrefab; // The ball to shoot
    public Transform shootPoint;  // The position from where the ball will be shot
    public float shootForce = 500f; // The force with which the ball will be shot
    public float shootInterval = 2f; // Time interval between shots

    void Start()
    {
        // Start the repeating shooting function
        InvokeRepeating("ShootBall", 0f, shootInterval);
    }

    void ShootBall()
    {// Instantiate the ball at the cannon's shoot point
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);

        // Apply force to the ball to shoot it downward
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.down * shootForce); // Apply force downward
        }

        // Destroy the ball after 3 seconds
        Destroy(ball, 1f);
    }
}
