using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class playerAnimationController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("Animator is working");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d"))
        {
            animator.SetBool("isRunning", true);
            Debug.Log("D key is pressed");
        }

        if (!Input.GetKey("d"))
        {
            animator.SetBool("isRunning", false);
            Debug.Log("is Running Stops");
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isJumping");
            Debug.Log("Jumping is working");
        }
    }
}