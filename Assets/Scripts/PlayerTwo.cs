using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTwo : PlayerController
{

    // Start is called before the first frame update
    public override void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetButtonDown("Jump2") && base.IsGrounded())
        {
            player.velocity = new Vector2(player.velocity.x, 10f);
        }


        float xAxis = Input.GetAxis("Horizontal2");
        player.velocity = new Vector2(xAxis * 5f, player.velocity.y);
    }


}
