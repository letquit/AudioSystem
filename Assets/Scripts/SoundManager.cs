using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace AudioSystem
{
    /// <summary>
    /// 音频管理器，负责管理和播放游戏中的音效
    /// 使用对象池模式来优化音频发射器的创建和销毁性能
    /// </summary>
    public class SoundManager : PersistentSingleton<SoundManager>
    {
        private IObjectPool<SoundEmitter> soundEmitterPool;
        private readonly List<SoundEmitter> activeSoundEmitters = new List<SoundEmitter>();
        public readonly Queue<SoundEmitter> FrequentSoundEmitters = new Queue<SoundEmitter>();
        
        [SerializeField] private SoundEmitter soundEmitterPrefab;
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxPoolSize = 100;
        [SerializeField] private int maxSoundInstances = 30;

        private void Start()
        {
            InitializePool();
        }
        
        /// <summary>
        /// 创建一个新的声音构建器实例
        /// </summary>
        /// <returns>声音构建器实例</returns>
        public SoundBuilder CreateSound() => new SoundBuilder(this);

        /// <summary>
        /// 销毁池中的音频发射器对象
        /// </summary>
        /// <param name="soundEmitter">要销毁的音频发射器</param>
        private void OnDestroyPoolObject(SoundEmitter soundEmitter)
        {
            Destroy(soundEmitter.gameObject);
        }
        
        /// <summary>
        /// 检查是否可以播放指定的声音数据
        /// 当达到最大声音实例数时，会停止最早的一个声音实例
        /// </summary>
        /// <param name="data">要播放的声音数据</param>
        /// <returns>如果可以播放则返回true，否则返回false</returns>
        public bool CanPlaySound(SoundData data)
        {
            if (activeSoundEmitters.Count >= maxSoundInstances)
            {
                // 如果已达到最大声音实例数，停止最早的声音实例
                if (activeSoundEmitters.Count > 0)
                {
                    activeSoundEmitters[0].Stop(); 
                }
                // 再次检查是否仍超过最大限制
                if (activeSoundEmitters.Count >= maxSoundInstances) return false;
        
                return true;
            }
            return true;
        }

        /// <summary>
        /// 从对象池中获取一个音频发射器实例
        /// </summary>
        /// <returns>音频发射器实例</returns>
        public SoundEmitter Get()
        {
            return soundEmitterPool.Get();
        }

        /// <summary>
        /// 将音频发射器返回到对象池中
        /// </summary>
        /// <param name="soundEmitter">要归还的音频发射器</param>
        public void ReturnToPool(SoundEmitter soundEmitter)
        {
            soundEmitterPool.Release(soundEmitter); 
        }
        
        /// <summary>
        /// 当音频发射器返回到池中时的回调方法
        /// </summary>
        /// <param name="soundEmitter">返回池中的音频发射器</param>
        private void OnReturnToPool(SoundEmitter soundEmitter)
        {
            soundEmitter.gameObject.SetActive(false);
            activeSoundEmitters.Remove(soundEmitter);
        }
        
        /// <summary>
        /// 当从池中取出音频发射器时的回调方法
        /// </summary>
        /// <param name="soundEmitter">从池中取出的音频发射器</param>
        private void OnTakeFromPool(SoundEmitter soundEmitter)
        {
            soundEmitter.gameObject.SetActive(true);
            activeSoundEmitters.Add(soundEmitter);
        }
        
        /// <summary>
        /// 创建新的音频发射器实例
        /// </summary>
        /// <returns>新创建的音频发射器实例</returns>
        private SoundEmitter CreateSoundEmitter()
        {
            var soundEmitter = Instantiate(soundEmitterPrefab);
            soundEmitter.gameObject.SetActive(false);
            return soundEmitter;
        }

        /// <summary>
        /// 初始化音频发射器对象池
        /// 设置池的创建、获取、归还和销毁回调方法
        /// </summary>
        private void InitializePool()
        {
            soundEmitterPool = new ObjectPool<SoundEmitter>(
                CreateSoundEmitter,
                OnTakeFromPool,
                OnReturnToPool,
                OnDestroyPoolObject,
                collectionCheck,
                defaultCapacity,
                maxPoolSize);
        }
    }
}
