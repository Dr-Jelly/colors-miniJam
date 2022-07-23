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
    [SerializeField] private Animator animator;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [Range(0.1f, 10)] [Tooltip("Velocity Changerate - Lower value makes the character movement smoother")]
    [SerializeField] private float Acceleration;

    // =====[ References ]====== //
    public void StartRecordingInput() => recorder.StartRecording();
    public void StopRecordingInput() => recorder.StopRecording();
    public void StartRePlayingInput() => rePlayer.StartRePlay(recorder.GetRecording());

    // =====[ Physics ]====== //

    public void MovementUpdate(Vector2 direction)
    {
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
        if (direction == Vector2.zero) isMoving = false;

        // Facing Direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) // X Axis is used
            xAxis = direction.x > 0 ? 1 : -1; // x größer als 0? also 1 // ansonsten -1

        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) // Y Axis is used
            yAxis = direction.y > 0 ? 1 : -1; // y größer als 0? also 1 // ansonsten -1

        animator.SetBool("IsMoving", isMoving);
        animator.SetInteger("yInput", yAxis);
        animator.SetInteger("xInput", xAxis);
    }


    // =====[ Events ]====== //

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
