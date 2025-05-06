using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Flip player based on move direction
        if (horizontalInput > 0.01f) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump() {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        anim.SetTrigger("Jump");
        grounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") {
            grounded = true;
        }
    }
}
