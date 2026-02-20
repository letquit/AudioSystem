namespace AudioSystem {
    using UnityEngine;

    /// <summary>
    /// 持久化单例基类，用于创建在场景切换时不会被销毁的单例对象
    /// </summary>
    /// <typeparam name="T">继承自Component的类型</typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component {
        /// <summary>
        /// 是否在Awake时自动取消父级关系，默认为true
        /// </summary>
        public bool AutoUnparentOnAwake = true;

        /// <summary>
        /// 存储单例实例的静态变量
        /// </summary>
        protected static T instance;

        /// <summary>
        /// 获取当前是否存在实例
        /// </summary>
        public static bool HasInstance => instance != null;
        
        /// <summary>
        /// 尝试获取当前实例，如果不存在则返回null
        /// </summary>
        /// <returns>单例实例或null</returns>
        public static T TryGetInstance() => HasInstance ? instance : null;

        /// <summary>
        /// 获取单例实例，如果不存在则自动创建
        /// </summary>
        public static T Instance {
            get {
                if (instance == null) {
                    instance = FindAnyObjectByType<T>();
                    if (instance == null) {
                        var go = new GameObject(typeof(T).Name + " Auto-Generated");
                        instance = go.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake() {
            InitializeSingleton();
        }

        /// <summary>
        /// 初始化单例模式的核心逻辑
        /// 处理对象层级关系、实例检查和DontDestroyOnLoad设置
        /// </summary>
        protected virtual void InitializeSingleton() {
            if (!Application.isPlaying) return;

            // 自动取消父级关系以避免在场景切换时被销毁
            if (AutoUnparentOnAwake) {
                transform.SetParent(null);
            }

            // 检查是否已有实例存在
            if (instance == null) {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else {
                // 如果已有实例且当前对象不是该实例，则销毁当前对象
                if (instance != this) {
                    Destroy(gameObject);
                }
            }
        }
    }
}
