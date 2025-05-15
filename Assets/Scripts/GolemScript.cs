using UnityEngine;

public class GolemScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private LayerMask wallLayer;
    private BoxCollider2D boxCollider;
    public int health = 10;
    public LogicScript logic;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.left;

        if (onWall()) {
            moveSpeed *= -1;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (health == 0) {
            Destroy(gameObject);

            if (gameObject.tag == "Golem Boss") {
                logic.gameWon();
            }
        }
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
