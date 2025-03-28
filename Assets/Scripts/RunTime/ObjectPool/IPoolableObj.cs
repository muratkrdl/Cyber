using UnityEngine.Pool;

public interface IPoolableObj<T> where T : class
{
    public void SetObjPool(IObjectPool<T> pool);
}
