using UnityEngine;

public class DashingStone : MonoBehaviour
{
    [SerializeField] private float dashPowerDuration = 20f; // seconds dash is enabled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement movement = collision.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.ActivateDashPowerUp(dashPowerDuration);
                Destroy(gameObject); // remove the stone
            }
        }
    }
}
