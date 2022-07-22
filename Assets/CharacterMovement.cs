using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
        MovementUpdate(InputController.Instance.Movement);
    }

    private void MovementUpdate(Vector2 direction)
    {
        direction.Normalize();

        rb.velocity = direction * (speed * 100 * Time.fixedDeltaTime);
    }
}
