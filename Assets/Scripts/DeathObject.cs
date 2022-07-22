using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour
{
    [SerializeField] private string Tag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tag))
            Kill(collision.gameObject);
    }

    private void Kill(GameObject character)
    {
        Destroy(character);
        GameController.Instance.ChangeState(GameController.GameState.Replay);
    }
}
