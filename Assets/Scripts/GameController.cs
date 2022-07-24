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
        if (InputController.Instance.Next)
        {
            CurrentChar().Die();
            Invoke("NextChar", 2f);
        }

        if (InputController.Instance.Undo)
        {
            UndoTurn();
        }

        if (InputController.Instance.Enter)
        {
            NextLevel(NextLevelName);
        }


        if (InputController.Instance.Restart)
        {
            SceneController.Instance.ReloadScene();
        }
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
    [SerializeField] public List<PlayableCharacter> CharacterList;
    [SerializeField] public int CurrentCharacterIndex;

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
        print("next");

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
    public string NextLevelName;

    public void UndoTurn()
    {
        if (CurrentChar() == null) return; //Leave if index is out of bounds

        if (CurrentChar().NearSpawn())
        {
            if (CurrentCharacterIndex < 1) return;
            CurrentCharacterIndex--;

            PaintObstacles(CurrentChar().Color);
            foreach (var character in CharacterList)
            {
                character.Reset();
            }
            for (int i = 0; i < CurrentCharacterIndex; i++)
            {
                CharacterList[i].StartRePlayingInput();
            }
            TurnEnded?.Invoke();
            CurrentChar().StartRecordingInput();
        }
        else
        {
            foreach (var character in CharacterList)
            {
                character.Reset();
            }
            for (int i = 0; i < CurrentCharacterIndex; i++)
            {
                CharacterList[i].StartRePlayingInput();
            }
            TurnEnded?.Invoke();
            CurrentChar().StartRecordingInput();
        }
    }

    public void NextLevel(string levelName)
    {
        SceneController.Instance.ChangeScene(levelName);
    }

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

    // ----------------- Goals
    public List<GoalController> goals;

    public void GoalReached()
    {
        if (CheckWinCondition())
        {
            foreach (var character in CharacterList)
            {
                if (!character.HasWon) character.Die();
            }
            Invoke("NextChar", 2f);
        }
        else
        {
            int numOfGoalsReached = 0;
            for (int i = 0; i <= CurrentCharacterIndex; i++)
            {
                if (i < CharacterList.Count && CharacterList[i].HasWon == true) numOfGoalsReached++;
            }
            if (numOfGoalsReached == CurrentCharacterIndex + 1)
            {
                Invoke("NextChar", 2f);
                print("won?");
            }
        }
    }

    public bool CheckWinCondition()
    {
        foreach (var goal in goals)
        {
            if (goal.HasBeenReached == false) return false;
        }
        return true;
    }

    // ----------------- Debug

    private void OnValidate()
    {
        goals = new List<GoalController>(FindObjectsOfType<GoalController>());
    }

}

[Serializable]
public struct ColorCapsule
{
    public ColorName name;
    public Color color;
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