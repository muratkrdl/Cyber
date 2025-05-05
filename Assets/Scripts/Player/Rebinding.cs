using UnityEngine;
using UnityEngine.InputSystem;

public class Rebinding : MonoBehaviour
{
    PlayerInputActions myPlayerInput;

    void Rebind()
    {
        if(Keyboard.current.tKey.wasPressedThisFrame)
        {
            myPlayerInput.GamePlay.Disable();
            myPlayerInput.UI.Enable();
        }

        // start listening device for rebind input
        myPlayerInput.Disable();
        myPlayerInput.GamePlay.Jump.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnComplete(callback => { Debug.Log(callback); callback.Dispose(); } )
            .Start();
    }

    private void SaveRebindings()
    {
        var binding = myPlayerInput.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("Bindings", binding);
    }
    private void LoadRebinding()
    {
        var binding = PlayerPrefs.GetString("Bindings");
        myPlayerInput.LoadBindingOverridesFromJson(binding);
    }
}
