using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player2Controller : PlayerController
{

    // Start is called before the first frame update
    public void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetButtonDown("Jump2") && base.IsGrounded())
        {
            player.velocity = new Vector3(player.velocity.x, 10f, 0);
        }


        float dirX = Input.GetAxis("Horizontal2");
        player.velocity = new Vector2(dirX * 5f, player.velocity.y);
    }
}
