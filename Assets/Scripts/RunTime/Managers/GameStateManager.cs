using System;
using RunTime.Extensions;

namespace RunTime.Managers
{
    public class GameStateManager : MonoSingleton<GameStateManager>
    {
        public Action OnGamePause;
        public Action OnGameResume;

        private GameStates currentState;

        private bool isGamePaused;

        public bool GetIsGamePaused => isGamePaused;

        private void OnEnable() 
        {
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

        private void OnDisable() 
        {
            OnGamePause -= GameStateManager_OnGamePause;
            OnGameResume -= GameStateManager_OnGameResume;
        }

    }
}
