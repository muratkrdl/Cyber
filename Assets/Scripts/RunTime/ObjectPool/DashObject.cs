using UnityEngine;
using UnityEngine.Pool;

public class DashObject : MonoBehaviour, IPoolableObj<DashObject>
{
    IObjectPool<DashObject> pool;

    public void SetObjPool(IObjectPool<DashObject> pool)
    {
        this.pool = pool;
    }

    

}
