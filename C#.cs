using UnityEngine;

public class PlayerController : MonoBehaviour
{
public float speed = 5f;
public float jumpForce = 10f;
private bool isGrounded;
private Rigidbody2D rb;

void Start()
{
rb = GetComponent<Rigidbody2D>();
}

void Update()
{
// Horizontal Movement
float moveInput = Input.GetAxis("Horizontal");
rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

// Jumping
if (isGrounded && Input.GetKeyDown(KeyCode.Space))
{
rb.velocity = Vector2.up * jumpForce;
isGrounded = false;
}
}

void OnCollisionEnter2D(Collision2D collision)
{
if (collision.gameObject.CompareTag("Ground"))
{
isGrounded = true;
}
}
}
  using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip player sprite based on movement direction
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Jumping
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnDrawGizmos()
    {
        // Visualize ground check in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}

