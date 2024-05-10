using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class PlayerController : MonoBehaviour
{
    [SerializeField] protected LayerMask jumpableGround;

    protected BoxCollider2D coll;
    protected Rigidbody2D player;
    protected AudioSource sounds;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected IPlayerState currentState = new PlayerIdle();
    protected Vector2 input = Vector2.zero;
    [SerializeField] public float speed;
    [SerializeField] private float height;

    public abstract void Start();
    public abstract void Update();

    protected void UpdateState()
    {
        IPlayerState newState = currentState.Tick(this, input);

        if (newState != null)
        {
            currentState.Exit(this);
            currentState = newState;
            newState.Enter(this);
        }
    }

    public void Animate(string param, bool toggle)
    {
        anim.SetBool(param, toggle);
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void DestroyState(IPlayerState state)
    {
        state = null;
    }

    public void Move(float xAxis)
    {
        player.velocity = new Vector2(xAxis * speed, player.velocity.y);
        sprite.flipX = false;
        if (xAxis < 0f) sprite.flipX = true;
    }

    public void Jump()
    {
        player.velocity = new Vector2(player.velocity.x, height);
    }

    public void PlaySound(bool order)
    {
        sounds.enabled = order;
    }
}

public interface IPlayerState
{
    public IPlayerState Tick(PlayerController player, Vector2 input);
    public void Enter(PlayerController player);
    public void Exit(PlayerController player);
}

public class PlayerIdle : IPlayerState
{
    public void Enter(PlayerController player)
    {
        return;
    }

    public IPlayerState Tick(PlayerController player, Vector2 input)
    {
        if ((input.y > 0f) && player.IsGrounded()) return new PlayerJumping();
        if (input.x != 0f) return new PlayerRunning();
        return null;
    }

    public void Exit(PlayerController player)
    {
        player.DestroyState(this);
        return;
    }
}

public class PlayerRunning : IPlayerState
{
    public void Enter(PlayerController player)
    {
        player.Animate("running", true);
        player.PlaySound(true);
        return;
    }

    public IPlayerState Tick(PlayerController player, Vector2 input)
    {
        if ((input.y > 0f) && player.IsGrounded()) return new PlayerJumping();
        if (input.x == 0f) return new PlayerIdle();
        player.Move(input.x);
        return null;
    }

    public void Exit(PlayerController player)
    {
        player.Animate("running", false);
        player.PlaySound(false);
        player.DestroyState(this);
        return;
    }
}

public class PlayerJumping : IPlayerState
{
    public void Enter(PlayerController player)
    {
        player.Animate("jumping", true);
        player.Jump();
        return;
    }

    public IPlayerState Tick(PlayerController player, Vector2 input)
    {
        if ((input.x != 0f) && player.IsGrounded()) return new PlayerRunning();
        if (player.IsGrounded()) return new PlayerIdle();
        player.Move(input.x);
        return null;
    }

    public void Exit(PlayerController player)
    {
        player.Animate("jumping", false);
        player.DestroyState(this);
        return;
    }
}