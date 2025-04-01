using UnityEngine;

public static class AnimationsID
{
    // Bools
    public static readonly int IsFalling = Animator.StringToHash("IsFalling");

    // Triggers
    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int Jump = Animator.StringToHash("Jump");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int Dash = Animator.StringToHash("Dash");
    public static readonly int Dead = Animator.StringToHash("Dead");

    // Floats
    public static readonly int Speed = Animator.StringToHash("Speed");
    public static readonly int AttackIndex = Animator.StringToHash("AttackIndex");
    public static readonly int LinearVelocityY = Animator.StringToHash("LinearVelocityY");
}
