using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnClickEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent Event;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
    }
}
