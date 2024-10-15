using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;

    public AnimationClip walkAnimation;
    public AnimationClip runAnimation;
    public AnimationClip jumpAnimation;

    private float currentSpeed;
    private Rigidbody rb;
    private Animation anim;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();

        // Adding animation clips to the Animation component
        anim.AddClip(walkAnimation, "Walk");
        anim.AddClip(runAnimation, "Run");
        anim.AddClip(jumpAnimation, "Jump");

        currentSpeed = walkSpeed;
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Shift key for running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
            if (!anim.IsPlaying("Run"))
            {
                anim.CrossFade("Run");
            }
        }
        else
        {
            currentSpeed = walkSpeed;
            if (!anim.IsPlaying("Walk"))
            {
               // anim.CrossFade("Walk");
            }
        }

        // Player movement
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(transform.position + move * currentSpeed * Time.deltaTime);

        // Stop animation if no movement
        if (move.magnitude == 0 && anim.isPlaying)
        {
            anim.Stop();
        }
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.CrossFade("Jump");
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
