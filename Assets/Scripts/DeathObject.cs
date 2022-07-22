using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayableCharacter c = collision.gameObject.GetComponent<PlayableCharacter>();
        if (c != null) Kill(c);
    }

    private void Kill(PlayableCharacter character)
    {
        character.Die();
        if (GameController.Instance.CurrentCharacterIndex < GameController.Instance.CharacterList.Count)
        {
            if (GameController.Instance.CharacterList[GameController.Instance.CurrentCharacterIndex] == character)
            {
                GameController.Instance.NextChar();
            }
        }
    }
}
