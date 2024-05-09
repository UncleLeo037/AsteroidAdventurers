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
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void Update()
    {
        input.y = Input.GetAxis("Jump");
        input.x = Input.GetAxis("Horizontal");
        base.UpdateState();
    }
}

