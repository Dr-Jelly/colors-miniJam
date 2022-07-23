using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    protected override void Awake()
    {
        base.Awake();
        AwakeCharacter();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CharacterUpdate();
    }

    // ----------------- Turns
    public enum GameState { PlayerTurn, Replay }
    public GameState State;

    public void ChangeState(GameState s)
    {
        if (State != s)
            State = s;
    }

    // ----------------- Character
    [SerializeField] private List<PlayableCharacter> CharacterList;
    [SerializeField] private int CurrentCharacterIndex;

    public PlayableCharacter CurrentChar()
    {
        if (CurrentCharacterIndex < CharacterList.Count)
        {
            return CharacterList[CurrentCharacterIndex];
        }
        return null;
    }

    public void AwakeCharacter()
    {
        CurrentChar()?.StartRecordingInput();
    }

    public void NextChar()
    {
        // Replay all previous characters.
        if (CurrentChar() == null) return; //Leave if index is out of bounds
        CurrentChar().StopRecordingInput();

        for (int i = 0; i <= CurrentCharacterIndex; i++)
        {
            CharacterList[i].Reset();
            CharacterList[i].StartRePlayingInput();
        }
        TurnEnded?.Invoke();

        CurrentCharacterIndex++;
        CurrentChar()?.StartRecordingInput();
    }

    public void CharacterUpdate()
    {
        CurrentChar()?.MovementUpdate(InputController.Instance.Movement);
    }

    // ----------------- Turns
    private UnityEvent TurnEnded = new UnityEvent();
    public void SubOnTurnEnd(UnityAction action) => TurnEnded.AddListener(action);
    public void UnSubOnTurnEnd(UnityAction action) => TurnEnded.RemoveListener(action);
}
