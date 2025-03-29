public interface IPlayerState
{
    void EnterState(PlayerFacade player);
    void UpdateState(PlayerFacade player);
    void FixedUpdateState(PlayerFacade player);
    void ExitState(PlayerFacade player);
}
