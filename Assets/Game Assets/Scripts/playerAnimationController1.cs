using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // Add this for pointer events

public class PlayerController1 : MonoBehaviour
{
    public TextMeshPro countText;
    private int coinCount = 0;

    Animator animator;
    Rigidbody rb;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    public bool isGrounded = true;

    public TextMeshPro DisplayText;
    public GameObject questionPanel;
    public GameObject ResetUI;

    // Add references for the UI buttons
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;

    // Variables to track button states
    private bool isLeftPressed;
    private bool isRightPressed;
    private bool isJumpPressed;

    public string gameOverSceneName = "GameOver";

    void Start()
    {
        UpdateCoinCountText();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Add button listeners for left button
        if (leftButton != null)
        {
            EventTrigger leftTrigger = leftButton.gameObject.GetComponent<EventTrigger>();
            if (leftTrigger == null)
                leftTrigger = leftButton.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.PointerDown;
            pointerDown.callback.AddListener((data) => { isLeftPressed = true; });
            leftTrigger.triggers.Add(pointerDown);

            EventTrigger.Entry pointerUp = new EventTrigger.Entry();
            pointerUp.eventID = EventTriggerType.PointerUp;
            pointerUp.callback.AddListener((data) => { isLeftPressed = false; });
            leftTrigger.triggers.Add(pointerUp);
        }

        // Add button listeners for right button
        if (rightButton != null)
        {
            EventTrigger rightTrigger = rightButton.gameObject.GetComponent<EventTrigger>();
            if (rightTrigger == null)
                rightTrigger = rightButton.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.PointerDown;
            pointerDown.callback.AddListener((data) => { isRightPressed = true; });
            rightTrigger.triggers.Add(pointerDown);

            EventTrigger.Entry pointerUp = new EventTrigger.Entry();
            pointerUp.eventID = EventTriggerType.PointerUp;
            pointerUp.callback.AddListener((data) => { isRightPressed = false; });
            rightTrigger.triggers.Add(pointerUp);
        }

        if (jumpButton != null)
        {
            EventTrigger jumpTrigger = jumpButton.gameObject.GetComponent<EventTrigger>();
            if (jumpTrigger == null)
                jumpTrigger = jumpButton.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.PointerDown;
            pointerDown.callback.AddListener((data) => { isJumpPressed = true; });
            jumpTrigger.triggers.Add(pointerDown);

            EventTrigger.Entry pointerUp = new EventTrigger.Entry();
            pointerUp.eventID = EventTriggerType.PointerUp;
            pointerUp.callback.AddListener((data) => { isJumpPressed = false; });
            jumpTrigger.triggers.Add(pointerUp);
        }

        // Add jump button listener
       /* if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(HandleJumpButtonClick);
            Flipmoment();
        }*/
    }

    void Update()
    {
        // Check for flip combinations
        if (isGrounded)
        {
            if (isRightPressed && isJumpPressed)
            {
                animator.SetTrigger("Isflip");
            }
            if (isLeftPressed && isJumpPressed)
            {
                animator.SetTrigger("Isflip");
            }
            if (!isLeftPressed && isJumpPressed)
            {
                HandleJumpButtonClick();
            }
            if (!isRightPressed && isJumpPressed)
            {
                HandleJumpButtonClick();
            }
        }

        HandleMovement();
        
    }



    void HandleMovement()
    {
        if (isLeftPressed)
        {
            animator.SetBool("isRunning", true);
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else if (isRightPressed)
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

    // New method to handle jump button click
    public void HandleJumpButtonClick()
    {
        if (isGrounded)
        {
            StartCoroutine(JumpWithDelay());
        }
    }

    IEnumerator JumpWithDelay()
    {
        animator.SetBool("isJumping", true);
        isGrounded = false;
        animator.SetBool("isIdle", false);
        yield return new WaitForSeconds(0.2f);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isIdle", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            coinCount++;
            UpdateCoinCountText();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(gameOverSceneName);
        }

        if (other.CompareTag("Wrong"))
        {
            DisplayText.text = "Wrong Answer";
            Debug.Log("Wrong Answer");
            ResetUI.SetActive(true);
            questionPanel.SetActive(false);
            rightButton.gameObject.SetActive(false);
            leftButton.gameObject.SetActive(false);
            jumpButton.gameObject.SetActive(false);
        }

        if (other.CompareTag("Right"))
        {
            questionPanel.SetActive(false);
            DisplayText.text = "Right Answer";
            Debug.Log("Right Answer");
        }
    }

    void UpdateCoinCountText()
    {
        countText.text = "Coins: " + coinCount.ToString();
    }
}