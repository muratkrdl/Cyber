using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float scrollSpeed;

    private Material material;

    private void Start() 
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        Vector2 input = new(GlobalUnits.Instance.GetPlayer().GetRigidbody.linearVelocityX, 0);
        material.mainTextureOffset += scrollSpeed * Time.deltaTime * input;
    }

}
