using DG.Tweening;
using RunTime.Data.UnityObjects;
using RunTime.Data.ValueObjects.DashObject;
using RunTime.Helpers;
using RunTime.Systems.ObjectPooling;
using UnityEngine;
using UnityEngine.Pool;

namespace RunTime.Objects
{
    public class DashObject : MonoBehaviour, IPoolableObj<DashObject>
    {
        private DashObjData _data;
        
        private Transform _objTransform;
        private SpriteRenderer _spriteRenderer;

        private IObjectPool<DashObject> pool;

        private void Awake()
        {
            GetReferences();
            SetData();
        }

        private void GetReferences()
        {
            _objTransform = transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void SetData()
        {
            _data = Resources.Load<CD_DashObj>("Data/CD_DashObj").DashObjData;
        }

        public void SetObjPool(IObjectPool<DashObject> pool)
        {
            this.pool = pool;
        }

        public void Initialize(SpriteRenderer renderer, Transform refTransform)
        {
            Color randomColor = MyHelpers.GetRandomColor();
            Color lerpColor = new(randomColor.r, randomColor.g, randomColor.b, 0);
        
            _spriteRenderer.sprite = renderer.sprite;
            _spriteRenderer.color = randomColor;

            _objTransform.position = renderer.transform.position;
            _objTransform.localScale = refTransform.localScale;

            _spriteRenderer.DOColor(lerpColor, _data.Duration)
                .SetEase(_data.EaseMode)
                .OnComplete(ReturnToPool);
        }

        private void ReturnToPool()
        {
            pool.Release(this);
        }

    }
}
