using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] private bool IsOpen;
    [SerializeField] private bool InitialState;
    [SerializeField] private Color OpenedColor;
    [SerializeField] private Color ClosedColor;

    private void Awake()
    {
        InitialState = IsOpen;
        SetColor();
        GameController.Instance.SubOnTurnEnd(ResetDoor);
    }

    public void OpenDoor()
    {
        if (IsOpen) return;

        IsOpen = true;
        doorCollider.enabled = false;

        SetColor();
    }

    public void CloseDoor()
    {
        if (!IsOpen) return;

        IsOpen = false; 
        doorCollider.enabled = true;

        SetColor();
    }

    public void ResetDoor()
    {
        if (InitialState == true) OpenDoor();
        else if (InitialState == false) CloseDoor();
    }

    public void SetColor()
    {
        if (IsOpen) GetComponent<SpriteRenderer>().color = OpenedColor;
        else GetComponent<SpriteRenderer>().color = ClosedColor;
    }

    private void OnDisable() => GameController.Instance.UnSubOnTurnEnd(ResetDoor);
}
