using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public class DashObject : MonoBehaviour, IPoolableObj<DashObject>
{
    [Header("Settings")]
    [SerializeField] private Ease easeMod;
    [SerializeField] private float duration;

    private SpriteRenderer spriteRenderer;

    private IObjectPool<DashObject> pool;

    public void SetObjPool(IObjectPool<DashObject> pool)
    {
        this.pool = pool;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(SpriteRenderer renderer, Transform refTransform)
    {
        Color randomColor = Extensions.GetRandomColor();
        Color lerpColor = new(randomColor.r, randomColor.g, randomColor.b, 0);
        
        spriteRenderer.sprite = renderer.sprite;
        spriteRenderer.color = randomColor;
        transform.position = renderer.transform.position;
        transform.localScale = refTransform.localScale;
        spriteRenderer.DOColor(lerpColor, duration).SetEase(easeMod).OnComplete(() => pool.Release(this));
    }

}
