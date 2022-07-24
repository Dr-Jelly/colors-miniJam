using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public PlayableCharacter Subject;
    public bool HasBeenReached;

    private void Awake()
    {
        ResetGoal();
    }
    private void Start()
    {
        GameController.Instance.SubOnTurnEnd(ResetGoal);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayableCharacter character = collision.gameObject.GetComponent<PlayableCharacter>();
        if (character == Subject) CompleteLevel();
    }

    public void CompleteLevel()
    {
        HasBeenReached = true;
        Subject.Win();

        GameController.Instance.GoalReached();
    }

    private void Continue() => GameController.Instance.NextChar();

    public void ResetGoal()
    {
        HasBeenReached = false;
    }

    private void OnDisable() => GameController.Instance.UnSubOnTurnEnd(ResetGoal);
}
