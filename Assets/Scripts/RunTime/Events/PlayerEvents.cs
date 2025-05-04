using System;
using UnityEngine;

namespace RunTime.Events
{
    public class PlayerEvents : MonoBehaviour
    {
        public static PlayerEvents Instance;

        private void Awake() 
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        
        // public Action<IPlayerState> OnStateChange;
        public Action OnResetAttackIndex;

    }
}
