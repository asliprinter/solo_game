using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim;

    private float horizontalInput;
    private bool facingRight = true;

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when changing direction
        if (horizontalInput > 0.01f && !facingRight)
        {
            facingRight = true;
            Flip();
        }
        else if (horizontalInput < -0.01f && facingRight)
        {
            facingRight = false;
            Flip();
        }

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (Input.GetKey(KeyCode.Space))
            Jump();

    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Jump()
    {
        anim.SetTrigger("jump");
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null; 
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
