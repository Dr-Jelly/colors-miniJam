using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnter2DEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent Event;
    [SerializeField] private string Tag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tag))
            Event.Invoke();
    }
}
