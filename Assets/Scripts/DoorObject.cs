using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] private bool IsOpen;

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
}
