using UnityEngine;
using TMPro;

public class GolemScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private LayerMask wallLayer;
    private BoxCollider2D boxCollider;
    public int health = 10;
    [SerializeField] private LogicScript logic;
    [SerializeField] private PlayerHealth playerHealth;
    public AudioSource hitSound;
    public Animator anim;
    public enum Note { A, B, C, D, E, F, G }
    public Note vulnerableNote;
    private TextMeshPro noteText;
    private Transform noteObject;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        noteText = GetComponentInChildren<TextMeshPro>();
        noteObject = transform.Find("GolemNote");
        GenerateRandomNote();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.left;

        // golem flip logic when it touched wall
        if (OnWall())
        {
            FlipGolem();
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void GenerateRandomNote()
    {
        // Pick a random number from 0 to 6 and convert it to a Note
        int rand = Random.Range(0, 7);
        vulnerableNote = (Note)rand;

        if (noteText != null)
            noteText.text = vulnerableNote.ToString();
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private void FlipGolem()
    {
        moveSpeed *= -1;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        noteObject.transform.localScale = new Vector3(-noteObject.transform.localScale.x, -noteObject.transform.localScale.y, -noteObject.transform.localScale.z);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When player collides with golem - damage player
        if (collision.gameObject.CompareTag("Player"))
        {
            hitSound.Play();
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
        // Flip direction golem is going
        else if (collision.gameObject.CompareTag("Golem"))
        {
            FlipGolem();
        }
    }
}
