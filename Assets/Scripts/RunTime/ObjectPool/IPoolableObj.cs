using UnityEngine.Pool;

public interface IPoolableObj<T> where T : class
{
    void SetObjPool(IObjectPool<T> pool);
}
