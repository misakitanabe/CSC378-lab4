using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBoss : MonoBehaviour
{
    private Transform player;
    private LogicScript logic;

    public bool isFlipped = false;
    public float health = 10;
    public float startingHealth = 10;

    [SerializeField] private AudioClip roarSound;
    [SerializeField] private AudioSource roarSource;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private AudioSource audioSource;
    private float roarCooldown;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        roarCooldown = Random.Range(5f, 10f);

        // Auto-assign player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("FireBoss could not find a GameObject with tag 'Player'.");
        }

        // Auto-assign logic
        logic = FindFirstObjectByType<LogicScript>();
        if (logic == null)
        {
            Debug.LogWarning("FireBoss could not find LogicScript in the scene.");
        }
        
        // the fillAmount dictates how much of healthbar is showing
        currentHealthBar.fillAmount = 1;
        totalHealthBar.fillAmount = 1;

    }

    void Update()
    {
        LookAtPlayer(); 

        roarCooldown -= Time.deltaTime;

        if (roarCooldown <= 0f)
        {
            if (roarSound != null && audioSource != null)
            {
                roarSource.PlayOneShot(roarSound);
            }

            roarCooldown = Random.Range(8f, 15f);
        }

        // update health in health bar
        currentHealthBar.fillAmount = health / startingHealth;

        if (health <= 0)
        {
            Destroy(gameObject);

            if (gameObject.CompareTag("Fire Boss") && logic != null)
            {
                logic.gameWon();
            }
        }
    }

    public void LookAtPlayer()
    {
        if (player == null) return;

        Vector3 scale = transform.localScale;

        if (player.position.x > transform.position.x)
        {
            scale.x = -Mathf.Abs(scale.x);
            isFlipped = false;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
            isFlipped = true;
        }

        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }
}
