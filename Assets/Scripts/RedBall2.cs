using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class RedBall2 : RedBall
{

    public PlayerController player;


    public override void BallInit()
    {
        
        base.BallInit();
        player = GameObject.Find("player").GetComponent<PlayerController>();

        GameManager.disabledRb += disableRb;

    }

    private void OnDisable()
    {
        GameManager.PoweredUp -= PowerUp;
        GameManager.disabledRb -= disabledRb;
    }

    public async void disableRb()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }
      
    }

    public override void ReactCollision(Collision2D collision)
    {

        direction = (player.rb.position - rb.position).normalized; 

    }


    private void OnCollisionStay2D(Collision2D collision)
    {

        direction = (player.rb.position - rb.position).normalized;

    }

}
