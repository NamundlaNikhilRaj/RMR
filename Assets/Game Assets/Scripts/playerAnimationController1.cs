using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Add this to use UI elements


public class PlayerController1 : MonoBehaviour
{
    public TextMeshPro countText; // UI Text element for displaying coin count
    private int coinCount = 0; // Coin counter

  

    Animator animator;
    Rigidbody rb;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    private bool isGrounded = true;

    // Add variables for UI buttons
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public TextMeshPro DisplayText;

    public GameObject questionPanel;
    public GameObject ResetUI;
    public GameObject leftBUtton1;
    public GameObject RightBUtton1;
    public GameObject UPBUtton1;

    // Variables to track button states
    private bool isLeftPressed = false;
    private bool isRightPressed = false;




    // Name of your game over scene
    public string gameOverSceneName = "GameOver";

    void Start()
    {

        // Initialize the coin count display
        UpdateCoinCountText();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //Debug.Log("Animator and Rigidbody initialized");

        // Add listeners to the buttons
        if (leftButton != null)
            leftButton.onClick.AddListener(() => StartLeftMovement());
        if (rightButton != null)
            rightButton.onClick.AddListener(() => StartRightMovement());
        if (jumpButton != null)
            jumpButton.onClick.AddListener(() => TriggerJump());

        // Add listeners for when buttons are released (requires additional setup in Unity)
        if (leftButton != null)
            leftButton.onClick.AddListener(() => StopLeftMovement());
        if (rightButton != null)
            rightButton.onClick.AddListener(() => StopRightMovement());
    }

    void Update()
    {
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

    // Methods to handle button presses
    public void StartLeftMovement()
    {
        isLeftPressed = true;
        isRightPressed = false;
    }

    public void StartRightMovement()
    {
        isRightPressed = true;
        isLeftPressed = false;
    }

    public void StopLeftMovement()
    {
        isLeftPressed = false;
    }

    public void StopRightMovement()
    {
        isRightPressed = false;
    }

    public void TriggerJump()
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
        yield return new WaitForSeconds(0.5f);
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

    // New method for trigger collision detection
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            // Increment the coin counter
            coinCount++;

            // Update the coin count text on the UI
            UpdateCoinCountText();
            Destroy(other.gameObject);

        }

        if (other.CompareTag("Enemy"))
        {
            //HandleGameOver();
            SceneManager.LoadScene(gameOverSceneName);
        }

        if (other.CompareTag("Wrong"))
        {
            DisplayText.text = "Wrong Answer";
            Debug.Log("Wrong Answer");
            ResetUI.SetActive(true);
            questionPanel.SetActive(false);
            leftBUtton1.SetActive(false);
            RightBUtton1.SetActive(false);
            UPBUtton1.SetActive(false);
            //HandleGameOver();
            // SceneManager.LoadScene(gameOverSceneName);
        }

        if (other.CompareTag("Right"))
        {
            questionPanel.SetActive(false);
            DisplayText.text = "Right Answer";
            Debug.Log("Right Answer");
            //HandleGameOver();
            //SceneManager.LoadScene(gameOverSceneName);
        }
    }




    // Update the UI text with the current coin count
    void UpdateCoinCountText()
    {
        countText.text = "Coins: " + coinCount.ToString();
        
    }
}






// New method to handle game over
/*private void HandleGameOver()
{
    // You can add death animation or particle effects here
    StartCoroutine(GameOverSequence());
}

// Coroutine for game over sequence
private IEnumerator GameOverSequence()
{
    // Disable player movement
    enabled = false;

    // You could trigger death animation here if you have one
    if (animator != null)
    {
        // Assuming you have a "death" animation
        // animator.SetTrigger("death");

        // Wait for animation to complete (adjust time as needed)
        yield return new WaitForSeconds(1f);
    }

    // Load game over scene
    SceneManager.LoadScene(gameOverSceneName);
}
*/
