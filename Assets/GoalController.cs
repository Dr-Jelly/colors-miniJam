using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public PlayableCharacter characterTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayableCharacter c = collision.gameObject.GetComponent<PlayableCharacter>();
        if (c == characterTarget) CompleteLevel();
    }

    public void CompleteLevel()
    {
        print("Yippieh!");
    }
}
