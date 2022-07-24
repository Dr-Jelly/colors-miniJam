using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private ColorName color;
    [SerializeField] private PlayableCharacter Subject;
    [SerializeField] private UnityEvent OnButtonActivate;
    [SerializeField] private UnityEvent OnButtonDeActivate;
    [SerializeField] private bool WasPressed = false;
    [SerializeField] private bool InitialState = false;
    [SerializeField] private Sprite ButtonNotPressedSprite;
    [SerializeField] private Sprite ButtonPressedSprite;


    private void Awake()
    {
        InitialState = WasPressed;
        ResetButton();
    }
    private void Start()
    {
        GameController.Instance.SubOnTurnEnd(ResetButton);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayableCharacter c = collision.gameObject.GetComponent<PlayableCharacter>();
        if (c == Subject) PressButton();
    }

    public void PressButton()
    {
        if (WasPressed)
        {
            OnButtonDeActivate?.Invoke();
            WasPressed = false;
            GetComponent<SpriteRenderer>().sprite = ButtonNotPressedSprite;
        }
        else
        {
            OnButtonActivate?.Invoke();
            WasPressed = true;
            GetComponent<SpriteRenderer>().sprite = ButtonPressedSprite;
        }
    }

    public void ResetButton()
    {
        if (InitialState == true)
        {
            WasPressed = true;
            GetComponent<SpriteRenderer>().sprite = ButtonPressedSprite;
        }
        else if (InitialState == false)
        {
            WasPressed = false;
            GetComponent<SpriteRenderer>().sprite = ButtonNotPressedSprite;
        }
    }

    private void OnDisable() => GameController.Instance.UnSubOnTurnEnd(ResetButton);


    // ===== Debug ===== //

    private void OnValidate()
    {
        foreach (var character in FindObjectsOfType<PlayableCharacter>())
        {
            if (character.Color == this.color)
            {
                Subject = character;
            }
        }
    }
}
