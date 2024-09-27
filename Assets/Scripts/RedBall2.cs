using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall2 : RedBall
{

    public PlayerController player;


 

    public override void BallInit()
    {
        base.BallInit();

        player = GameObject.Find("player").GetComponent<PlayerController>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("GreenBall"))
        {

            direction = (player.rb.position - rb.position).normalized; 

        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.OnPlayerTouchRedBall();
        }
    }

}
