using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float direction;
    [SerializeField] private float speed;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim;
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
        // Need to exclude player as an object to hit and projectiles ...
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("MusicNote"))
        {
            return;
        }

        // Collision-Enabled Objects
        hit = true;
        boxCollider.enabled = false; // here so that our projectiles don't move the mobs
        anim.SetTrigger("explode"); //parameter for animation

        // decreases golem health if hit
        if (collision.gameObject.CompareTag("Golem"))
        {
            GolemScript golem = collision.gameObject.GetComponent<GolemScript>(); // get the game object that was hit and adjusts it's health
            if (golem != null)
            {
                // golem hit with vulnerable note - damage 5 for now
                if (gameObject.name.Substring(5,1).StartsWith(golem.vulnerableNote.ToString()))
                    golem.health -= 5;
                // any other note - damage 1
                else
                    golem.health -= 1;
                golem.anim.SetTrigger("Golem Damaged");
            }
        }
        // decreases boss health if hit
        else if (collision.gameObject.CompareTag("Fire Boss"))
        {
            FireBoss boss = collision.gameObject.GetComponent<FireBoss>(); // get the game object that was hit and adjusts it's health
            if (boss != null)
            {
                boss.health -= 1;
            }
        }
         else if (collision.gameObject.CompareTag("NormalFireBoss"))
        {
            NormalFireBoss boss = collision.gameObject.GetComponent<NormalFireBoss>(); // get the game object that was hit and adjusts it's health
            if (boss != null)
            {
                boss.health -= 1;
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
