using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayableCharacter character = collision.gameObject.GetComponent<PlayableCharacter>();
        if (character != null) Kill(character);
    }

    private void Kill(PlayableCharacter character)
    {
        character.Die();

        if (GameController.Instance.CurrentChar() == character)
            Invoke("Continue", 2f);
    }

    private void Continue() => GameController.Instance.NextChar();
}
