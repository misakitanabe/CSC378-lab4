using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float direction;
    [SerializeField] private float speed;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim; // 
    private float lifetime; // duration of how long our projectile will continue to keep going

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Need to exclude player as an object to hit
        if (collision.gameObject.CompareTag("Player")){
            return;
        } 

        // Collision-Enabled Objects
        hit = true; 
        boxCollider.enabled = false; // here so that our projectiles don't move the mobs
        anim.SetTrigger("explode"); //parameter for animation

        if (collision.gameObject.CompareTag("Golem"))
        {
            GolemScript golem = collision.gameObject.GetComponent<GolemScript>(); // get the game object that was hit and adjusts it's health
            if (golem != null)
            {
                golem.health -= 1; 
            }
        }
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    // Purpose: Get's rid of gameObject *doesn't destory, simply deactivates*, for projectiles on crash
    private void Deactivate() 
    {
        gameObject.SetActive(false);
    }
}
