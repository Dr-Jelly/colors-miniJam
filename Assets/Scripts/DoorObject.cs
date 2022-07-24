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
        ResetDoor();
    }
    private void Start()
    {
        GameController.Instance.SubOnTurnEnd(ResetDoor);
    }

    public void OpenDoor()
    {
        IsOpen = true;
        doorCollider.enabled = false;
        GetComponent<SpriteRenderer>().color = OpenedColor;
    }

    public void CloseDoor()
    {
        IsOpen = false; 
        doorCollider.enabled = true;
        GetComponent<SpriteRenderer>().color = ClosedColor;
    }

    public void ResetDoor()
    {
        if (InitialState == true) OpenDoor();
        else if (InitialState == false) CloseDoor();
    }

    private void OnDisable() => GameController.Instance.UnSubOnTurnEnd(ResetDoor);
}
