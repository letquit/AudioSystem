using UnityEngine;

namespace AudioSystem
{
    /// <summary>
    /// 音效构建器，用于构建和播放音效
    /// </summary>
    public class SoundBuilder
    {
        private readonly SoundManager soundManager;
        private SoundData soundData;
        private Vector3 position = Vector3.zero;
        private bool randomPitch;

        /// <summary>
        /// 初始化音效构建器实例
        /// </summary>
        /// <param name="soundManager">音效管理器实例</param>
        public SoundBuilder(SoundManager soundManager)
        {
            this.soundManager = soundManager;
        }

        /// <summary>
        /// 设置音效数据
        /// </summary>
        /// <param name="soundData">音效数据</param>
        /// <returns>当前音效构建器实例</returns>
        public SoundBuilder WithSoundData(SoundData soundData)
        {
            this.soundData = soundData;
            return this;
        }

        /// <summary>
        /// 设置音效播放位置
        /// </summary>
        /// <param name="position">音效播放位置</param>
        /// <returns>当前音效构建器实例</returns>
        public SoundBuilder WithPosition(Vector3 position)
        {
            this.position = position;
            return this;
        }
        
        /// <summary>
        /// 启用随机音调功能
        /// </summary>
        /// <returns>当前音效构建器实例</returns>
        public SoundBuilder WithRandomPitch()
        {
            this.randomPitch = true;
            return this;
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        public void Play()
        {
            // 检查是否可以播放音效
            if (!soundManager.CanPlaySound(soundData)) return;
            
            SoundEmitter soundEmitter = soundManager.Get();
            soundEmitter.Initialize(soundData);
            soundEmitter.transform.position = position;
            soundEmitter.transform.parent = SoundManager.Instance.transform;

            // 应用随机音调设置
            if (randomPitch)
            {
                soundEmitter.WithRandomPitch();
            }

            // 将频繁播放的音效发射器加入队列
            if (soundData.frequentSound)
            {
                soundManager.FrequentSoundEmitters.Enqueue(soundEmitter);
            }
            soundEmitter.Play();
        }
    }
}
