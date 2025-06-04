using System;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    private float direction;
    [SerializeField] private float speed;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim; // 
    private float lifetime; // duration of how long our projectile will continue to keep going
    [SerializeField] private AudioClip impactSound;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer; // Assign in inspector



    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Need to exclude player as an object to hit and projectiles ...
        // if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("MusicNote")){
        //     return;
        // } 
        if (collision.gameObject.CompareTag("Fire Boss") || collision.gameObject.CompareTag("NormalFireBoss") ){
            return;
        }
        if (!hit)
        {
            hit = true;
            boxCollider.enabled = false; // Prevent further collisions
            anim.SetTrigger("explode");

            // Play sound
            if (impactSound != null)
                audioSource.PlayOneShot(impactSound);
        }
     
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>(); // get the game object that was hit and adjusts it's health
            player.TakeDamage(1f);
        }
    }
    // Handles MusicNote trigger hit
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MusicNote"))
        {
            Destroy(gameObject);
            Debug.Log("Projectile hit MusicNote trigger.");
        }
    }


    public void SetDirection(float direction)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on ProjectileEnemy!", this);
            return;
        }

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        // Flip sprite visually
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction > 0;
        }
    }



    // Purpose: Get's rid of gameObject *doesn't destory, simply deactivates*, for projectiles on crash
    private void Deactivate() 
    {
        gameObject.SetActive(false);
    }
    // For instaniated objects
    private void Destroy()
    {
        Destroy(gameObject); 
    }


}
