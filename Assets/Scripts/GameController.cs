using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public enum GameState { PlayerTurn, Replay }
    public GameState State;

    public void ChangeState(GameState s)
    {
        if (State != s)
            State = s;
    }
}
