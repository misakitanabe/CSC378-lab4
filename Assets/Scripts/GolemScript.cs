using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GolemScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private LayerMask wallLayer;
    private BoxCollider2D boxCollider;
    public float health = 5;
    public float startingHealth = 5;
    [SerializeField] private LogicScript logic;
    [SerializeField] private PlayerHealth playerHealth;
    public AudioSource hitSound;
    public Animator anim;
    public enum Note { A, B, C, D, E, F, G }
    public Note vulnerableNote;
    private TextMeshPro noteText;
    private Transform noteObject;
    public static Image BackgroundImage; // Shared by all prefab instances
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        noteText = GetComponentInChildren<TextMeshPro>();
        noteObject = transform.Find("GolemNote");
        GenerateRandomNote();
        // the fillAmount dictates how many of the lives are showing
        currentHealthBar.fillAmount = 1;
        totalHealthBar.fillAmount = 1;
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

        // update golem health in health bar
        currentHealthBar.fillAmount = health / startingHealth;
        Debug.Log(health / startingHealth);

        // golem killed logic
        if (health <= 0)
        {
            Destroy(gameObject);
            if (BackgroundImage != null)
            {
                BackgroundImage.StartCoroutine(LightenBackground(BackgroundImage, 0.1f));
            }
        }
    }


    // function to lighten background that gets called every time golem is killed 
    private IEnumerator LightenBackground(Image image, float amount)
    {
        Color original = image.color;
        Color target = new Color(
            Mathf.Min(original.r + amount, 1f),
            Mathf.Min(original.g + amount, 1f),
            Mathf.Min(original.b + amount, 1f),
            original.a
        );

        float elapsed = 0f;
        float duration = 1f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            image.color = Color.Lerp(original, target, elapsed / duration);
            yield return null;
        }

        image.color = target;
    }

    void GenerateRandomNote()
    {
        // Pick a random number from 0 to 6 and convert it to a Note
        int rand = Random.Range(0, 7);
        vulnerableNote = (Note)rand;

        if (noteText != null)
            noteText.text = vulnerableNote.ToString();
    }

    // returns true if golem collides with anything in wall layer
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private void FlipGolem()
    {
        moveSpeed *= -1;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        noteObject.transform.localScale = new Vector3(-noteObject.transform.localScale.x, noteObject.transform.localScale.y, noteObject.transform.localScale.z);
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
