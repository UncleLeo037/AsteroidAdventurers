using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class PlayerController : MonoBehaviour
{
    [SerializeField] protected LayerMask jumpableGround;

    protected BoxCollider2D coll;
    public Rigidbody2D player;

    protected IPlayerState currentState = new PlayerIdle();

    protected new Vector2 input;

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
        //idle animation
        return;
    }

    public IPlayerState Tick(PlayerController player, Vector2 input)
    {
        if ((input.y > 0f) && player.IsGrounded()) return new PlayerJumping();
        if (input.x > 0f) return new PlayerRunning();
        return null;
    }

    public void Exit(PlayerController player)
    {
        //Destroy(this);
        return;
    }
}

public class PlayerRunning : IPlayerState
{
    public void Enter(PlayerController player)
    {
        //running animation
        return;
    }

    public IPlayerState Tick(PlayerController player, Vector2 input)
    {
        if ((input.y > 0f) && player.IsGrounded()) return new PlayerJumping();
        if (input.x == 0f) return new PlayerIdle();
        player.player.velocity = new Vector2(input.x * 5f, player.player.velocity.y);
        return null;
    }

    public void Exit(PlayerController player)
    {
        //Destroy(this);
        return;
    }
}

public class PlayerJumping : IPlayerState
{
    public void Enter(PlayerController player)
    {
        //running animation
        player.player.velocity = new Vector2(player.player.velocity.x, 10f);
        return;
    }

    public IPlayerState Tick(PlayerController player, Vector2 input)
    {
        if ((input.x > 0f) && player.IsGrounded()) return new PlayerRunning();
        if (player.IsGrounded()) return new PlayerIdle();
        return null;
    }

    public void Exit(PlayerController player)
    {
        //Destroy(this);
        return;
    }
}