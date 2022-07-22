using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePlayer : MonoBehaviour
{
    public PlayableCharacter Subject;
    public Recorder recorder;
    public Queue<Vector2> inputQueue;

    public bool IsReplaying = false;

    public void StartRePlay()
    {
        if (recorder.inputRecording?.Count > 0)
        {
            inputQueue = new Queue<Vector2>(recorder.inputRecording);
            IsReplaying = true;
        }
    }

    public void FixedUpdate()
    {
        if (IsReplaying)
        {
            if (inputQueue?.Count > 0)
                Subject.MovementUpdate(inputQueue.Dequeue());
            else
            {
                print("stop");
                Subject.Die();
                IsReplaying = false;
            }
        }
    }
}
