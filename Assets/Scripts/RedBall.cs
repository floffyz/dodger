using System;
using System.Threading.Tasks;
using UnityEngine;

public class RedBall : MonoBehaviour
{
    public float speed = 3f;
    public Vector2 direction;
    public Rigidbody2D rb;
    public bool crazy;

    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;

    private void Start()
    {
        BallInit();
        GameManager.PoweredUp += PowerUp;
    }

    public virtual void BallInit()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = spriteRenderer.transform.localScale;
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
            ReactCollision(collision);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.OnPlayerTouchRedBall();
        }
    }

    public virtual void ReactCollision(Collision2D collision)
    {
        direction = Vector2.Reflect(direction, collision.contacts[0].normal);
    }

    public void Spawn()
    {
        transform.position = new Vector2(UnityEngine.Random.Range(-9f, 9f), UnityEngine.Random.Range(-4f, 4f));
        direction = UnityEngine.Random.insideUnitCircle.normalized;
    }

    public async void PowerUp()
    {
        // Ativa o PowerUp
        spriteRenderer.transform.localScale /= 2;

        // Aguarda 10 segundos
        await WaitForSecondsAsync(3f);

        // Reverte o efeito do PowerUp
        spriteRenderer.transform.localScale = originalScale;
    }

    async Task WaitForSecondsAsync(float delay)
    {
        await Task.Delay(TimeSpan.FromSeconds(delay));
    }
}


