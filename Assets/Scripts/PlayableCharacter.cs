using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayableCharacter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private RePlayer rePlayer;
    [SerializeField] private Recorder recorder;
    [SerializeField] private SpawnPointController spawnPoint;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [Range(0.1f, 10)] [Tooltip("Velocity Changerate - Lower value makes the character movement smoother")]
    [SerializeField] private float Acceleration;

    [Header("Other Parameters")]
    [SerializeField] public ColorName Color;
    [SerializeField] public bool IsDead = false;
    [SerializeField] public bool HasWon = false;

    // =====[ References ]====== //
    public void StartRecordingInput() => recorder.StartRecording();
    public void StopRecordingInput() => recorder.StopRecording();
    public void StartRePlayingInput() => rePlayer.StartRePlay(recorder.GetRecording());
    public bool NearSpawn()
    {
        if (Vector2.Distance(transform.position, spawnPoint.transform.position) <= 0.5)
        {
            return true;
        }
        return false;
    }

    // =====[ Physics ]====== //

    public void MovementUpdate(Vector2 direction)
    {
        if (IsDead || HasWon) return;
        direction.Normalize();
        Face(direction);

        Vector2 vCurrent = direction * (speed * 100 * Time.fixedDeltaTime);
        rb.velocity = Vector2.MoveTowards(rb.velocity, vCurrent, Acceleration);
    }

    public void Face(Vector2 direction)
    {
        int xAxis = 0;
        int yAxis = 0;
        bool isMoving = false;

        // Is Moving
        if (direction != Vector2.zero) isMoving = true;

        // Facing Direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) // X Axis is used
            xAxis = direction.x > 0 ? 1 : -1; // x größer als 0? also 1 // ansonsten -1

        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) // Y Axis is used
            yAxis = direction.y > 0 ? 1 : -1; // y größer als 0? also 1 // ansonsten -1

        animator.SetBool("IsMoving", isMoving);
        animator.SetInteger("yInput", yAxis);
        animator.SetInteger("xInput", xAxis);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayableCharacter>(out PlayableCharacter otherCharacter))
        {
            this.Die();
            otherCharacter.Die();

            if (this == GameController.Instance.CurrentChar())
                Invoke("Undo", 2f);
        }
    }
    private void Undo() => GameController.Instance.UndoTurn();

    // =====[ Events ]====== //

    public void Stop()
    {
        rb.velocity = Vector2.zero;
        Face(Vector2.zero);
    }

    public void Reset()
    {
        IsDead = false;
        HasWon = false;
        Stop();
        animator.SetBool("IsMoving", false);
        animator.SetBool("Dead", false);
        animator.SetBool("Won", false);
        spawnPoint.Reset();
        rePlayer.Reset();
        recorder.StopRecording();
    }

    public void Die()
    {
        IsDead = true; 
        Stop();
        animator.SetBool("Dead", true);
    }

    public void Win()
    {
        HasWon = true;
        Stop();
        animator.SetBool("Won", true);
    }

    // =====[ Debug ]====== //
    private void OnValidate()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        foreach (var spawn in FindObjectsOfType<SpawnPointController>())
        {
            if (spawn.color == this.Color)
            {
                spawnPoint = spawn;
            }
        }
    }
}
