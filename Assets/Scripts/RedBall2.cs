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


    public override void ReactCollision(Collision2D collision)
    {

        direction = (player.rb.position - rb.position).normalized; 

    }


    private void OnCollisionStay2D(Collision2D collision)
    {

        direction = (player.rb.position - rb.position).normalized;

    }

}
