using UnityEngine;
using System.Collections;

public class GreenBall : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 direction;
    private Rigidbody2D rb;

    private bool crazy;
    public GameObject redBallRef;
    public GameObject redBall2Ref;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("RedBall"))
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            crazy = Random.Range(0, 2) == 0;

            if (crazy)
            {
                GameObject redBall2 = Instantiate(redBall2Ref);
            }
            else
            {
                GameObject redBall = Instantiate(redBallRef);
            }

            GameManager.Instance.Score(this);
        }
    }

    
    public void Spawn()
    {
        transform.position = new Vector2(Random.Range(-9f, 9f), Random.Range(-4f, 4f));
        direction = Random.insideUnitCircle.normalized;
    }

    public IEnumerator Respawn()
    {
        gameObject.SetActive(false); 
        yield return new WaitForSeconds(0.5f); 
        Spawn();
        gameObject.SetActive(true); 
    }
}
