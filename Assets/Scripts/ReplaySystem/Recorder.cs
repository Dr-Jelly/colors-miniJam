using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public Queue<Vector2> inputRecording;
    public bool IsRecording;

    private void FixedUpdate()
    {
        if (IsRecording) RecordInput(InputController.Instance.Movement);
    }

    [ContextMenu("StartRecording")]
    public void StartRecording()
    {
        if (inputRecording == null)
        {
            inputRecording = new Queue<Vector2>();
            IsRecording = true;
        }
        else print("Recorder already has an input recording!");
    }

    [ContextMenu("StopRecording")]
    public void StopRecording()
    {
        if (IsRecording)
        {
            IsRecording = false;
        }
        else print("Recorder is already not recording!");
    }

    public void RecordInput(Vector2 input)
    {
        inputRecording.Enqueue(input);
        print(input);
    }

    [ContextMenu("DrawDebug")]
    public void DrawDebug()
    {
        Vector2 lastPosition = Vector2.zero;
        foreach (var input in inputRecording)
        {
            if (input == Vector2.zero) break;//Skips all empty inputs (decreases lines drawn)

            Vector2 nextPosition = lastPosition + (input * (Time.fixedDeltaTime*5));
            Debug.DrawLine(lastPosition, nextPosition, Color.red, 3f);

            lastPosition = nextPosition;
        }
    }
}
