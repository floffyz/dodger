using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    private Animator animator;

    private Quaternion rotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        if (!GameManager.Instance.over)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb.velocity = movement * moveSpeed;

            if (movement == Vector2.zero)
            {
                animator.SetBool("moving", false);
                transform.rotation = rotation;
            }
            else
            {
                rotation = transform.rotation;
                animator.SetBool("moving", true);
            }
        }

       

        if (GameManager.Instance.over)
        {
            animator.SetBool("dead", true);
        }
        
    }
}
