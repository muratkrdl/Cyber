using UnityEngine;

public class GlobalUnits : MonoBehaviour
{
    public static GlobalUnits Instance;

    [SerializeField] private Player player;

    private void Awake() 
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Player GetPlayer()
    {
        return player;
    }

}
