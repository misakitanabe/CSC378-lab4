using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Dash Settings")]
    [SerializeField] private GameObject dashSlider; 
    [SerializeField] private float dashPower = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private bool dashPowerActive = false;
    public float dashPowerTimer = 0f;
    public float dashPowerDuration;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput;
    private bool isDashing;
    private float dashTimer;
    private float lastDashTime = -Mathf.Infinity;
    private PlayerPowerUp powerUp;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        powerUp = GetComponent<PlayerPowerUp>();

        // hide dash slider
        dashSlider.SetActive(false);
    }

    private void Update()
    {
        // if dash power is active 
        if (dashPowerActive)
        {
            dashPowerTimer -= Time.deltaTime; // update how much time left for dash power

            // dash power up ran out
            if (dashPowerTimer <= 0f)
            {
                dashPowerActive = false;
                dashPowerTimer = 0f;
                dashSlider.SetActive(false); // hide bar
            }
            
            // If we're dashing, ignore all other movement
            if (isDashing)
            {
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0f)
                {
                    isDashing = false;
                    body.gravityScale = 7;
                }
                return;
            }
        }
        
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player based on movement direction
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // DASH
        if (dashPowerActive && Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown)
        {
            Dash();
            return;
        }

        // Movement
        if (wallJumpCoolDown > 0.2f)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 7;
            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                Jump();
        }
        else
        {
            wallJumpCoolDown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("Jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.linearVelocity = new Vector2(-Math.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.linearVelocity = new Vector2(-Math.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCoolDown = 0;
        }
    }

    private void Dash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        lastDashTime = Time.time;

        float dashDirection = transform.localScale.x; // 1 for right, -1 for left
        body.linearVelocity = new Vector2(dashDirection * dashPower, 0);
        body.gravityScale = 0;
    }

    public void ActivateDashPowerUp(float duration)
    {
        dashPowerActive = true;
        dashPowerTimer = duration;
        dashPowerDuration = duration;
        powerUp.PowerUp(); // plays animation and sound
        dashSlider.SetActive(true); // show bar
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0, Vector2.down,
            0.1f, groundLayer
        );
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center, 
            boxCollider.bounds.size, 
            0, new Vector2(transform.localScale.x, 0), 
            0.1f, wallLayer
        );
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return !onWall() && !isDashing;
    }
}
