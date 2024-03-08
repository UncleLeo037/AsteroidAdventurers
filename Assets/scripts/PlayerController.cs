using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            player.velocity = new Vector3(0, 10f, 0);
        }


        float dirX = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(dirX * 5f, player.velocity.y);
    }
}
