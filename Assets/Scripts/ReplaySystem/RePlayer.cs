using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePlayer : MonoBehaviour
{
    [SerializeField] private PlayableCharacter Subject;
    [SerializeField] private Queue<Vector2> inputRecording;
    [SerializeField] private bool IsReplaying = false;

    public void StartRePlay(Queue<Vector2> recording)
    {
        if (recording?.Count > 0)
        {
            inputRecording = new Queue<Vector2>(recording); //Copy instead of reference
            IsReplaying = true;
        }
    }

    public void FixedUpdate()
    {
        if (IsReplaying)
        {
            if (inputRecording?.Count > 0)
                Subject.MovementUpdate(inputRecording.Dequeue());
            else
            {
                Subject.Die();
                IsReplaying = false;
            }
        }
    }
}
