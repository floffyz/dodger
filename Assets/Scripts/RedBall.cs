using UnityEngine;

public class RedBall : MonoBehaviour
{
    public float speed = 3f;
    public Vector2 direction;
    public Rigidbody2D rb;
    public bool crazy; 

    private SpriteRenderer spriteRenderer;

    

    private void Start()
    {
    
        BallInit();
        
    }

    public virtual void BallInit()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Spawn();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.over)
        {
            rb.velocity = direction * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("GreenBall"))
        {
        

            direction = Vector2.Reflect(direction, collision.contacts[0].normal);

        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.OnPlayerTouchRedBall();
        }
    }

    public void Spawn()
    {


        transform.position = new Vector2(Random.Range(-9f, 9f), Random.Range(-4f, 4f));
        direction = Random.insideUnitCircle.normalized;
    }
}
