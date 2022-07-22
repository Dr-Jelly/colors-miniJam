using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public PlayableCharacter Subject;

    public void Reset()
    {
        Subject.transform.position = this.transform.position;
    }
}
