using UnityEngine;

public class NormalFireBoss : MonoBehaviour
{
    public Transform player;

	public bool isFlipped = false;
	public int health = 10;
	[SerializeField] private LogicScript logic;
	[SerializeField] private AudioClip roarSound;
	[SerializeField] private AudioSource roarSource;
    [SerializeField] private AudioClip outroSound;
    [SerializeField] private AudioSource outroSource;
    [SerializeField] private GameObject Fire_Boss;



	private AudioSource audioSource;
	private float roarCooldown;

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

		if (health <= 0)
        {
            if (gameObject.CompareTag("NormalFireBoss"))
            {
                outroSource.PlayOneShot(outroSound);

                // Spawn enraged boss at the same position and orientation
                GameObject enragedBoss = Instantiate(Fire_Boss, transform.position, Quaternion.identity);
                enragedBoss.transform.localScale = transform.localScale;
            }

            Destroy(gameObject);
        }
	}

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		roarCooldown = Random.Range(5f, 10f); // randomize first roar delay
	}


	public void LookAtPlayer()
	{
		if (player == null) return;

		Vector3 scale = transform.localScale;
        int offset = 2;
		if (player.position.x > (transform.position.x + offset))
		{
			// Player is to the right — face right (negative X scale)
			scale.x = -Mathf.Abs(scale.x);
			isFlipped = false;
		}
		else
		{
			// Player is to the left — face left (positive X scale)
			scale.x = Mathf.Abs(scale.x);
			isFlipped = true;
		}

		transform.localScale = scale;
	}





    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
