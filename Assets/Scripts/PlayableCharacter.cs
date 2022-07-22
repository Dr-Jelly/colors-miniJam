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

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [Range(0.1f, 10)] [Tooltip("Velocity Changerate - Lower value makes the character movement smoother")]
    [SerializeField] private float Acceleration;


    public void MovementUpdate(Vector2 direction)
    {
        direction.Normalize();

        Vector2 vCurrent = direction * (speed * 100 * Time.fixedDeltaTime);
        rb.velocity = Vector2.MoveTowards(rb.velocity, vCurrent, Acceleration);
    }

    public void Die()
    {
        rb.velocity = Vector2.zero;
    }
}
