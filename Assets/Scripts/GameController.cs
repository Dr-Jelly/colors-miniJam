using System;
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

        foreach (var character in CharacterList)
        {
            character.Reset();
        }
        for (int i = 0; i <= CurrentCharacterIndex; i++)
        {
            CharacterList[i].StartRePlayingInput();
        }
        TurnEnded?.Invoke();

        CurrentCharacterIndex++;
        if (CurrentChar() == null)
        {
            PaintObstacles(ColorName.RAINBOW);
            return;
        }

        CurrentChar().StartRecordingInput();
        PaintObstacles(CurrentChar().Color);
    }

    public void CharacterUpdate()
    {
        CurrentChar()?.MovementUpdate(InputController.Instance.Movement);
    }

    // ----------------- Turns
    private UnityEvent TurnEnded = new UnityEvent();
    public void SubOnTurnEnd(UnityAction action) => TurnEnded.AddListener(action);
    public void UnSubOnTurnEnd(UnityAction action) => TurnEnded.RemoveListener(action);

    // ----------------- Colors
    public List<ColorCapsule> Colors;

    public Material GetColorMaterial(ColorName name)
    {
        foreach (var color in Colors)
        {
            if (color.name == name) return color.material;
        }
        print("No Color found");
        return null;
    }

    // ----------------- TileMaps
    public GameObject Obstacles;

    public void PaintObstacles(ColorName colorName)
    {
        Obstacles.gameObject.GetComponent<Renderer>().material = GetColorMaterial(colorName);
    }
}

[Serializable]
public struct ColorCapsule
{
    public ColorName name;
    public Material material;
}

public enum ColorName
{
    Green,
    Blue,
    Purple,
    Pink,
    Red,
    Orange,
    Yellow,
    RAINBOW
}