using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePlayer : MonoBehaviour
{
    public PlayableCharacter Subject;
    public Recorder recorder;

    public bool switcher = false;

    public void FixedUpdate()
    {
        if (switcher)
        {
            if (recorder.inputRecording?.Count > 0)
                Subject.MovementUpdate(recorder.inputRecording.Dequeue());
            else
            {
                Subject.Die();
                switcher = false;
            }
        }
    }
}
