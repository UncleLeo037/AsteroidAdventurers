using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerOne : PlayerController
{

    // Start is called before the first frame update
    public override void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        player = GetComponent<Rigidbody2D>();
        sounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetButtonDown("Jump") && base.IsGrounded())
        {
            player.velocity = new Vector2(player.velocity.x, 10f);
        }

        float xAxis = Input.GetAxis("Horizontal");
        if (player.velocity.x == 0f || !base.IsGrounded())
        {
            sounds.enabled = false;
        }
        else
        {
            sounds.enabled = true;
        }
        player.velocity = new Vector2(xAxis * 5f, player.velocity.y);
    }
}

