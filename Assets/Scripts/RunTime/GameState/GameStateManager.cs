using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public Action OnGamePause;
    public Action OnGameResume;

    private GameStates currentState;

    private bool isGamePaused;

    public bool GetIsGamePaused => isGamePaused;

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
        currentState = GameStates.Pause;
        isGamePaused = true;
    }
    private void GameStateManager_OnGameResume()
    {
        currentState = GameStates.Resume;
        isGamePaused = false;
    }

    private void OnDestroy() 
    {
        OnGamePause -= GameStateManager_OnGamePause;
        OnGameResume -= GameStateManager_OnGameResume;
    }

}
