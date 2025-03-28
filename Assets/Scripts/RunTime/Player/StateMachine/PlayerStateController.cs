using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    private Player player;

    private IPlayerState currentPlayerState;

    private void Awake() 
    {
        player = GetComponent<Player>();
    }

    private void Start() 
    {
        currentPlayerState = new PlayerIdleState();
        currentPlayerState.EnterState(player);
    }

    public void SetPlayerState(IPlayerState newState)
    {
        currentPlayerState.ExitState(player);
        currentPlayerState = newState;
        currentPlayerState.EnterState(player);
    }

    private void Update() 
    {
        currentPlayerState.UpdateState(player);
    }

    private void FixedUpdate() 
    {
        currentPlayerState.FixedUpdateState(player);
    }

}
