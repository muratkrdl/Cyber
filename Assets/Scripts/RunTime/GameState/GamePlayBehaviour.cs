using System;
using UnityEngine;

public class GamePlayBehaviour : MonoBehaviour
{
    private bool gamePaused;

    public bool GetGamePaused
    {
        get => gamePaused;
    }

    protected virtual void Start() 
    {
        GameStateManager.Instance.OnGamePause += GameStateManager_OnGamePause;
        GameStateManager.Instance.OnGameResume += GameStateManager_OnGameResume;
    }

    protected virtual void GameStateManager_OnGamePause()
    {
        gamePaused = true;
    }
    protected virtual void GameStateManager_OnGameResume()
    {
        gamePaused = false;
    }

    protected virtual void OnDestroy() 
    {
        GameStateManager.Instance.OnGamePause -= GameStateManager_OnGamePause;
    }

}
