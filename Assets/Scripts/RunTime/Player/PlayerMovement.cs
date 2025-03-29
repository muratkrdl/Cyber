using UnityEngine;

public class PlayerMovement
{
    private PlayerFacade playerFacade;

    private float velocityX;

    public PlayerMovement(PlayerFacade playerFacade)
    {
        this.playerFacade = playerFacade;
    }

    public void MoveHandle()
    {
        float horizontaMovement = playerFacade.GetPlayerMovementInput().x;
        velocityX = horizontaMovement * playerFacade.GetMoveSpeed() * Time.fixedDeltaTime;
        playerFacade.SetLinearVelocityX(velocityX);
    }

}
