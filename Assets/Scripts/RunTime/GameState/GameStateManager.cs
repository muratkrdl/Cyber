using System;
using UnityEngine;

public enum GameState
{
    none,
    Play,
    Pause,
    Resume,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public Action OnGamePause;
    public Action OnGameResume;

    private GameState currentState;

    private void Awake() 
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        OnGamePause += GameStateManager_OnGamePause;
        OnGameResume += GameStateManager_OnGameResume;
    }

    private void GameStateManager_OnGamePause()
    {
        currentState = GameState.Pause;
    }
    private void GameStateManager_OnGameResume()
    {
        currentState = GameState.Resume;
    }

    private void OnDestroy() 
    {
        OnGamePause -= GameStateManager_OnGamePause;
        OnGameResume -= GameStateManager_OnGameResume;
    }

}
