using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] private bool IsOpen;
    [SerializeField] private bool InitialState;

    private void Awake()
    {
        InitialState = IsOpen;
        GameController.Instance.SubOnTurnEnd(ResetDoor);
    }

    public void OpenDoor()
    {
        if (IsOpen) return;

        IsOpen = true;
        doorCollider.enabled = false;
        //Do Animation stuff
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void CloseDoor()
    {
        if (!IsOpen) return;

        IsOpen = false; 
        doorCollider.enabled = true;
        //Do other Animation stuff
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void ResetDoor()
    {
        if (InitialState == true) OpenDoor();
        else if (InitialState == false) CloseDoor();
    }

    private void OnDisable() => GameController.Instance.UnSubOnTurnEnd(ResetDoor);
}
