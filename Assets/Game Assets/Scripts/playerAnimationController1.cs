using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class playerAnimationController1 : MonoBehaviour
{
    Animator animator;
    //public Rigidbody rb;  // Reference to the Rigidbody component
    public float moveSpeed = 5f; // Speed at which the object moves
    public float rotationSpeed = 10f;  // Speed at which the object rotates
    public float upforce;
   // public Animator animator;  // Reference to the Animator component
    // Start is called before the first frame update
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
            animator.SetTrigger("isJumping");
            StartCoroutine(StopRunningAnimationAfterDelay());
            // Debug.Log("Jumping is working");
           // transform.position += Vector3.up * upforce * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
             animator.SetTrigger("isIdle");
            // animator.SetBool("isRunning", false);
            // Debug.Log("is Running Stops");
        }
        IEnumerator StopRunningAnimationAfterDelay()
        {
            yield return new WaitForSeconds(0.5f); // Wait for 1 second
            transform.position += Vector3.up * upforce * Time.deltaTime;
        }
    }
}