using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayableCharacter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public RePlayer rePlayer;
    [SerializeField] public Recorder recorder;
    [SerializeField] public SpawnPointController spawnPoint;
    [SerializeField] public Animator animator;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [Range(0.1f, 10)] [Tooltip("Velocity Changerate - Lower value makes the character movement smoother")]
    [SerializeField] private float Acceleration;


    public void MovementUpdate(Vector2 direction)
    {
        direction.Normalize();
        Face(direction);

        Vector2 vCurrent = direction * (speed * 100 * Time.fixedDeltaTime);
        rb.velocity = Vector2.MoveTowards(rb.velocity, vCurrent, Acceleration);
    }

    public void Face(Vector2 direction)
    {
        // Is Moving
        if (direction != Vector2.zero) animator.SetBool("IsMoving", true);
        else animator.SetBool("IsMoving", false);

        // Facing Direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0) animator.SetInteger("xInput", 1);
            else animator.SetInteger("xInput", -1);
            animator.SetInteger("yInput", 0);
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0) animator.SetInteger("yInput", 1);
            else animator.SetInteger("yInput", -1);
            animator.SetInteger("xInput", 0);
        }
    }

    public void Reset()
    {
        animator.SetBool("Dead", false);
        spawnPoint.Reset();
    }

    public void Die()
    {
        rb.velocity = Vector2.zero;
        Face(Vector2.zero);
        animator.SetBool("Dead", true);
    }
}
