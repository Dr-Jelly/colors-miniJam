using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    [SerializeField] private Queue<Vector2> recording;
    [SerializeField] private bool IsRecording;

    public Queue<Vector2> GetRecording() => recording;

    private void FixedUpdate()
    {
        if (IsRecording) RecordInput(InputController.Instance.Movement);
    }

    private void RecordInput(Vector2 input) => recording.Enqueue(input);

    public void StartRecording()
    {
        if (recording == null)
        {
            recording = new Queue<Vector2>();
            IsRecording = true;
        }
        else print("Recorder already has an input recording!");
    }

    public void StopRecording()
    {
        if (IsRecording)
        {
            IsRecording = false;
        }
        else print("Recorder is already not recording!");
    }


    [ContextMenu("DrawDebug")]
    public void DrawDebug()
    {
        Vector2 lastPosition = Vector2.zero;
        foreach (var input in recording)
        {
            if (input == Vector2.zero) break;//Skips all empty inputs (decreases lines drawn)

            Vector2 nextPosition = lastPosition + (input * (Time.fixedDeltaTime*5));
            Debug.DrawLine(lastPosition, nextPosition, Color.red, 3f);

            lastPosition = nextPosition;
            print("debug");
        }
    }
}
