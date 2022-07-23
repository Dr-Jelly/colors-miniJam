using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public PlayableCharacter characterTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayableCharacter character = collision.gameObject.GetComponent<PlayableCharacter>();
        if (character == characterTarget) CompleteLevel();
    }

    public void CompleteLevel()
    {
        characterTarget.Win();
        Invoke("Continue", 2f);
    }

    private void Continue() => GameController.Instance.NextChar();
}
