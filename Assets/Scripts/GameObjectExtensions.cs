using UnityEngine;

namespace AudioSystem
{
    /// <summary>
    /// 为GameObject提供扩展方法的静态类
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// 获取指定类型的组件，如果不存在则添加该组件
        /// </summary>
        /// <typeparam name="T">要获取或添加的组件类型，必须继承自Component</typeparam>
        /// <param name="gameObject">目标游戏对象</param>
        /// <returns>已存在的组件实例或新添加的组件实例</returns>
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            // 检查组件是否存在，如果不存在则添加该组件
            if (!component) component = gameObject.AddComponent<T>();
            
            return component;
        }
    }
}
