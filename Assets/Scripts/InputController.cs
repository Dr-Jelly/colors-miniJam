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
    public bool LeftClick;
    public bool RightClick;

    public void OnMove(InputAction.CallbackContext callback)
    {
        Movement = callback.ReadValue<Vector2>();
    }
    public void OnLeftMouse(InputAction.CallbackContext callback)
    {
        LeftClick = callback.ReadValue<bool>();
    }
    public void OnRightMouse(InputAction.CallbackContext callback)
    {
        RightClick = callback.ReadValue<bool>();
    }
}
