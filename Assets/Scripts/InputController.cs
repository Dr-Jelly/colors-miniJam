using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("References")]
    public PlayerInput Input;
    [Header("Input parameters")]
    public Vector2 Movement;
    public bool LeftClick => Input.actions["LeftClick"].WasPerformedThisFrame();
    public bool RightClick => Input.actions["RightClick"].WasPerformedThisFrame();

    public void OnMove(InputAction.CallbackContext callback)
    {
        Movement = callback.ReadValue<Vector2>();
    }
}
