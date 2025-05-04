using UnityEngine.Pool;

namespace RunTime.Systems.ObjectPooling
{
    public interface IPoolableObj<T> where T : class
    {
        void SetObjPool(IObjectPool<T> pool);
    }
}
