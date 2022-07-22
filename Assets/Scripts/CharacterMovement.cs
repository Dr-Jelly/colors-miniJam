using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [Range(0.1f, 10)] [Tooltip("Velocity Changerate - Lower value makes the character movement smoother")]
    [SerializeField] private float Acceleration;

    private void FixedUpdate()
    {
        MovementUpdate(InputController.Instance.Movement);
    }

    private void MovementUpdate(Vector2 direction)
    {
        direction.Normalize();

        Vector2 vCurrent = direction * (speed * 100 * Time.fixedDeltaTime);
        rb.velocity = Vector2.MoveTowards(rb.velocity, vCurrent, Acceleration);
    }
}
