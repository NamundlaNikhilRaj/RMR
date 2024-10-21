using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f; // Force applied for jumping
    private bool isGrounded = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>(); // Get Rigidbody component for physics
        Debug.Log("Animator and Rigidbody initialized");
    }

    void Update()
    {
        HandleMovement();
        //HandleJump();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            StartCoroutine(JumpWithDelay()); // Start coroutine for delayed jump
        }
    }

    void HandleMovement()
    {
        // Horizontal movement (left-right)
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isRunning", true);
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunning", true);
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

   IEnumerator JumpWithDelay()
    {
        // Trigger jump animation
        animator.SetBool("isJumping", true);
        isGrounded = false; // Prevent double jumps
        animator.SetBool("isIdle", false);
        // Wait for 0.5 seconds to match the animation timing
        yield return new WaitForSeconds(0.5f);

        // Apply the upward force for jumping after the delay
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

     /*void HandleJump()
     {
         // Jump logic
         if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
         {
             rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply upward force
             animator.SetBool("isJumping", true);
             isGrounded = false; // Prevent double jumps
         }
     }*/

    private void OnCollisionEnter(Collision collision)
    {
        // Check if player has landed on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isIdle", true);
        }
    }
}



/*using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class playerAnimationController1 : MonoBehaviour
{
    Animator animator;
    public float moveSpeed = 5f; // Speed at which the object moves
    public float rotationSpeed = 10f;  // Speed at which the object rotates
    public float upforce;
  
    void Start()
    {
        
        animator = GetComponent<Animator>();
        Debug.Log("Animator is working");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isRunning", true);
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            // Rotate the GameObject to face 90 degrees on the Y-axis
            Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunning", true);
            //Debug.Log("D key is pressed");
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp("a"))
        {
            animator.SetBool("isRunning", false);
            // Debug.Log("is Running Stops");
        }
        if (Input.GetKeyUp("d"))
        {
            animator.SetBool("isRunning", false);
            // Debug.Log("is Running Stops");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isIdle", false);
            StartCoroutine(StopRunningAnimationAfterDelay());
        }
        if (Input.GetKeyUp("space"))
        {
            StartCoroutine(StopRunningAnimationAfterDelay1());
        }
        IEnumerator StopRunningAnimationAfterDelay()
         {
             yield return new WaitForSeconds(0.5f); // Wait for 1 second
             transform.position += Vector3.up * upforce * Time.deltaTime;
         }

        IEnumerator StopRunningAnimationAfterDelay1()
        {
            yield return new WaitForSeconds(0.5f); // Wait for 1 second
                animator.SetBool("isRunning", true);  
                animator.SetBool("isIdle", true);
                animator.SetBool("isJumping", false);
        }
    }
}*/