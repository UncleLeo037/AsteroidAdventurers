using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class PlayerController : MonoBehaviour
{
    [SerializeField] protected LayerMask jumpableGround;

    protected BoxCollider2D coll;
    protected Rigidbody2D player;
    protected float dirX;

    public abstract void Start();

    public abstract void Update();

    protected bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
