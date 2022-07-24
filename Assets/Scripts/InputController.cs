using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : Singleton<InputController>
{
    [Header("References")]
    public PlayerInput Input;
    [Header("Input parameters")]
    public Vector2 Movement;
    public bool LeftClick => Input.actions["LeftClick"].WasPerformedThisFrame();
    public bool RightClick => Input.actions["RightClick"].WasPerformedThisFrame();
    public bool Enter => Input.actions["Enter"].WasPerformedThisFrame();
    public bool Next => Input.actions["Next"].WasPerformedThisFrame();
    public bool Undo => Input.actions["Undo"].WasPerformedThisFrame();
    public bool Restart => Input.actions["Restart"].WasPerformedThisFrame();

    public void OnMove(InputAction.CallbackContext callback)
    {
        Movement = callback.ReadValue<Vector2>();
    }
}
