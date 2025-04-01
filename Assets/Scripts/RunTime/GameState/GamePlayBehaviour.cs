using UnityEngine;

public abstract class GamePlayBehaviour : MonoBehaviour
{
    protected virtual void Start() 
    {
        GameStateManager.Instance.OnGamePause += GameStateManager_OnGamePause;
        GameStateManager.Instance.OnGameResume += GameStateManager_OnGameResume;
    }

    protected abstract void GameStateManager_OnGamePause();
    protected abstract void GameStateManager_OnGameResume();

    protected virtual void OnDestroy() 
    {
        GameStateManager.Instance.OnGamePause -= GameStateManager_OnGamePause;
        GameStateManager.Instance.OnGameResume -= GameStateManager_OnGameResume;
    }

}
