using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class GameManager : Singleton<GameManager>
{
    public PlayerController playerController;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    public int timeCountDown = 30;

    private void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(Countdown());
    }
    private IEnumerator Countdown()
    {
        while (timeCountDown > 0)
        {
            yield return new WaitForSeconds(1f);
            timeCountDown--;
            Debug.Log("Remaining time: " + timeCountDown);
        }
        UpdateGameState(GameState.Win);
        Debug.Log("Countdown finished!");
    }

    public void UpdateGameState(GameState gameState)
    {
        state = gameState;
        switch (gameState)
        {
            case GameState.Decide:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
        }

        OnGameStateChanged?.Invoke(state);
    }

    public void PlayerDie()
    {
        Debug.Log("Call");
        UpdateGameState(GameState.Lose);
    }
}
