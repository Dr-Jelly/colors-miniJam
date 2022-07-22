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
    public List<PlayableCharacter> CharacterList;
    public int CurrentCharacterIndex;

    public void AwakeCharacter()
    {
        CharacterList[CurrentCharacterIndex].recorder.StartRecording();
    }

    public void NextChar()
    {
        if (CurrentCharacterIndex >= CharacterList.Count) return;
        CharacterList[CurrentCharacterIndex].recorder.StopRecording();
        CharacterList[CurrentCharacterIndex].spawnPoint.Reset();
        CharacterList[CurrentCharacterIndex].rePlayer.switcher = true;
        CurrentCharacterIndex++;

        if (CurrentCharacterIndex >= CharacterList.Count) return;
        TurnEnded?.Invoke();
        CharacterList[CurrentCharacterIndex].recorder.StartRecording();
    }

    public void CharacterUpdate()
    {
        if (CurrentCharacterIndex >= CharacterList.Count) return;
        CharacterList[CurrentCharacterIndex].MovementUpdate(InputController.Instance.Movement);
    }

    // ----------------- Character
    private UnityEvent TurnEnded = new UnityEvent();
    public void SubOnTurnEnd(UnityAction action) => TurnEnded.AddListener(action);
    public void UnSubOnTurnEnd(UnityAction action) => TurnEnded.RemoveListener(action);
}
