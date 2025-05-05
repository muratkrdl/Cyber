using RunTime.Extensions;
using UnityEngine.Events;

namespace RunTime.Events
{
    public class CoreGameEvents : MonoSingleton<CoreGameEvents>
    {
        
        public UnityAction onGameStart;
        public UnityAction onGamePause;
        public UnityAction onGameResume;
        public UnityAction onGameQuit;
        public UnityAction onReset;

        public UnityAction onEnableGameplayInput;
        public UnityAction onDisableGameplayInput;

    }
}
