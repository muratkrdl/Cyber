using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public Action<IPlayerState> OnStateChange;
    public Action OnResetAttackIndex;
}
