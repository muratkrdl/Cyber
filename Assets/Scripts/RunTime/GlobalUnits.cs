using UnityEngine;

public class GlobalUnits : MonoBehaviour
{
    public static GlobalUnits Instance;

    [SerializeField] private PlayerFacade player;

    private void Awake() 
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public PlayerFacade GetPlayer()
    {
        return player;
    }

}
