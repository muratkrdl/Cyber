using UnityEngine;

namespace RunTime.Extensions
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private bool dontDestroyOnLoad = false;
        
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();

                    if (_instance == null)
                    {
                        GameObject singletonObj = new GameObject(typeof(T).Name);
                        _instance = singletonObj.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }
        
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if (dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
