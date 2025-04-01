using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float scrollSpeed;

    private Material material;
    private Vector2 input = Caches.Zero2;

    private void Start() 
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        input.x = GlobalUnits.Instance.GetPlayer().GetLinearVelocity().x;
        material.mainTextureOffset += scrollSpeed * Time.deltaTime * input;
    }

}
