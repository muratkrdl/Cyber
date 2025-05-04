using DG.Tweening;
using RunTime.Systems.ObjectPooling;
using UnityEngine;
using UnityEngine.Pool;

namespace RunTime.Objects
{
    public class DashObject : MonoBehaviour, IPoolableObj<DashObject>
    {
        [Header("Settings")]
        [SerializeField] private Ease easeMod;
        [SerializeField] private float duration;

        private Transform objTransform;
        private SpriteRenderer spriteRenderer;

        private IObjectPool<DashObject> pool;

        private void Awake()
        {
            objTransform = transform;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetObjPool(IObjectPool<DashObject> pool)
        {
            this.pool = pool;
        }

        public void Initialize(SpriteRenderer renderer, Transform refTransform)
        {
            Color randomColor = Extensions.MyExtensions.GetRandomColor();
            Color lerpColor = new(randomColor.r, randomColor.g, randomColor.b, 0);
        
            spriteRenderer.sprite = renderer.sprite;
            spriteRenderer.color = randomColor;

            objTransform.position = renderer.transform.position;
            objTransform.localScale = refTransform.localScale;

            spriteRenderer.DOColor(lerpColor, duration)
                .SetEase(easeMod)
                .OnComplete(() => ReturnToPool());
        }

        private void ReturnToPool()
        {
            pool.Release(this);
        }

    }
}
