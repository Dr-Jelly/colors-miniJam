using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private UnityEvent OnButtonPress;
    [SerializeField] private bool WasPressed = false;
    [SerializeField] private Sprite ButtonPressedSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayableCharacter c = collision.gameObject.GetComponent<PlayableCharacter>();
        if (c != null) Activate();
    }

    public void Activate()
    {
        OnButtonPress?.Invoke();
        WasPressed = true;
        GetComponent<SpriteRenderer>().sprite = ButtonPressedSprite;
    }
}
