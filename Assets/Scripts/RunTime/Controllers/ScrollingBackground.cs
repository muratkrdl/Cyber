using RunTime.Helpers;
using UnityEngine;

namespace RunTime.Controllers
{
    public class ScrollingBackgroundController : MonoBehaviour
    {
        private Material material;
        private float scrollSpeed;
        private Vector2 scrollPosition = Caches.Zero2;
        
        private void Start() 
        {
            material = GetComponent<SpriteRenderer>().material;
        }
        
        private void Update()
        {
            material.mainTextureOffset += scrollSpeed * Time.deltaTime * scrollPosition;
        }

        public void SetDir(float x)
        {
            scrollPosition.x = x;
        }

        public void SetScrollSpeed(float scrollSpeed)
        {
            this.scrollSpeed = scrollSpeed;
        }
    
    }
}
