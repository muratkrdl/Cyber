using UnityEngine;
using UnityEngine.Pool;

namespace RunTime.Systems.ObjectPooling
{
    public abstract class ObjectPoolBase<T> : MonoBehaviour where T : Component, IPoolableObj<T>
    {
        public static ObjectPoolBase<T> Instance;

        [SerializeField] private int defaultCapacity;
        [SerializeField] private int maxCapacity;

        [SerializeField] protected T prefab;

        private IObjectPool<T> pool;

        public IObjectPool<T> GetPool => pool;

        private void Awake() 
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            pool = new ObjectPool<T>
            (
                Create,
                OnGet,
                OnRelease,
                Destroy,
                true,
                defaultCapacity,
                maxCapacity
            );
        }

        protected virtual T Create()
        {
            T obj = Instantiate(prefab, transform.position, Quaternion.identity);
            obj.gameObject.SetActive(false);
            obj.SetObjPool((ObjectPool<T>)pool);
            obj.transform.SetParent(transform);
            return obj;
        }
        protected virtual void OnGet(T obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = transform.position;
        }
        protected virtual void OnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }
        protected virtual void Destroy(T obj)
        {
            Destroy(obj.gameObject);
        }

        private void OnDestroy() 
        {
            pool.Clear();
        }

    }
}
