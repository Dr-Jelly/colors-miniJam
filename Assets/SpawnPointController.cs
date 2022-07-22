using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public PlayableCharacter Subject;

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        Subject.transform.position = this.transform.position;
    }
}
