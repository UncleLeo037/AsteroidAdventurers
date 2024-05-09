using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class PlayerController : MonoBehaviour
{
    [SerializeField] protected LayerMask jumpableGround;

    protected BoxCollider2D coll;
    public Rigidbody2D player;
    public AudioSource sounds;
    public Animator anim;
    public SpriteRenderer sprite;

    protected IPlayerState currentState = new PlayerIdle();
    protected Vector2 input = Vector2.zero;
    public float speed;
    public float height;

    public abstract void Start();
    public abstract void Update();

    public void UpdateState()
    {
        IPlayerState newState = currentState.Tick(this, input);

        if (newState != null)
        {
            currentState.Exit(this);
            currentState = newState;
            newState.Enter(this);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void DestroyState(IPlayerState state)
    {
        state = null;
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
        //player.anim.SetBool("idle", true);
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
        //player.anim.SetBool("idle", false);
        player.DestroyState(this);
        return;
    }
}

public class PlayerRunning : IPlayerState
{
    public void Enter(PlayerController player)
    {
        player.anim.SetBool("running", true);
        player.sounds.enabled = true;
        return;
    }

    public IPlayerState Tick(PlayerController player, Vector2 input)
    {
        if ((input.y > 0f) && player.IsGrounded()) return new PlayerJumping();
        if (input.x == 0f) return new PlayerIdle();

        player.sprite.flipX = false;
        if (input.x < 0f) player.sprite.flipX = true;
        player.player.velocity = new Vector2(input.x * player.speed, player.player.velocity.y);
        return null;
    }

    public void Exit(PlayerController player)
    {
        player.anim.SetBool("running", false);
        player.sounds.enabled = false;
        player.DestroyState(this);
        return;
    }
}

public class PlayerJumping : IPlayerState
{
    public void Enter(PlayerController player)
    {
        player.anim.SetBool("jumping", true);
        player.player.velocity = new Vector2(player.player.velocity.x, player.height);
        return;
    }

    public IPlayerState Tick(PlayerController player, Vector2 input)
    {
        if ((input.x != 0f) && player.IsGrounded()) return new PlayerRunning();
        if (player.IsGrounded()) return new PlayerIdle();
        player.player.velocity = new Vector2(input.x * player.speed, player.player.velocity.y);
        return null;
    }

    public void Exit(PlayerController player)
    {
        player.anim.SetBool("jumping", false);
        player.DestroyState(this);
        return;
    }
}