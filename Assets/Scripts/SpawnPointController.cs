using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public ColorName color;
    public PlayableCharacter Subject;

    private void Awake() => Reset();
    public void Reset()
    {
        Vector3 newPosition = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        Subject.transform.position = newPosition;
    }

    // ===== Debug ===== //

    private void OnValidate()
    {
        foreach (var character in FindObjectsOfType<PlayableCharacter>())
        {
            if (character.Color == this.color)
            {
                Subject = character;
            }
        }
    }
}
