using UnityEngine;

public class PlayerMovement
{
    private PlayerFacade playerFacade;

    private float velocityX;
    private float horizontaMovement;

    public PlayerMovement(PlayerFacade playerFacade)
    {
        this.playerFacade = playerFacade;
    }

    public void MoveHandle()
    {
        horizontaMovement = playerFacade.GetPlayerMovementInput().x;
        velocityX = horizontaMovement * playerFacade.GetMoveSpeed() * Time.fixedDeltaTime;
        playerFacade.SetLinearVelocityX(velocityX);
    }

}
