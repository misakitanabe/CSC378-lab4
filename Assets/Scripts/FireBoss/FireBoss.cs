using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : MonoBehaviour
{

	public Transform player;

	public bool isFlipped = false;
	public int health = 10;
	[SerializeField] private LogicScript logic;
	[SerializeField] private AudioClip roarSound;
	[SerializeField] private AudioSource roarSource;
	private AudioSource audioSource;
	private float roarCooldown;

	void Update()
    {

		roarCooldown -= Time.deltaTime;

		if (roarCooldown <= 0f)
		{
			if (roarSound != null && audioSource != null)
			{
				roarSource.PlayOneShot(roarSound);
			}

			// Set a new random time until the next roar
			roarCooldown = Random.Range(8f, 15f);
		}

        if (health <= 0)
        {
            Destroy(gameObject);

            if (gameObject.CompareTag("Fire Boss"))
            {
                logic.gameWon();
            }
        }

    }
	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		roarCooldown = Random.Range(5f, 10f); // randomize first roar delay
	}


	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }

}