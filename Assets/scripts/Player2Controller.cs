using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player2Controller : MonoBehaviour
{
    private Rigidbody2D player;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    public void Start()
    {
        player = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetButtonDown("Jump2") && IsGrounded())
        {
            player.velocity = new Vector3(0, 10f, 0);
        }


        float dirX = Input.GetAxis("Horizontal2");
        player.velocity = new Vector2(dirX * 5f, player.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
