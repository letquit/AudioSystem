using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AudioSystem
{
    /// <summary>
    /// 声音发射器组件，用于播放音频并管理音频源的生命周期
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour
    {
        /// <summary>
        /// 当前绑定的声音数据
        /// </summary>
        public SoundData Data { get; private set; }
        
        private AudioSource audioSource;
        private Coroutine playingCoroutine;

        private void Awake()
        {
            audioSource = gameObject.GetOrAdd<AudioSource>();
        }

        /// <summary>
        /// 播放声音
        /// 如果当前有正在播放的协程，则先停止它
        /// </summary>
        public void Play()
        {
            if (playingCoroutine != null)
            {
                StopCoroutine(playingCoroutine);
            }
            
            audioSource.Play();
            playingCoroutine = StartCoroutine(WaitForSoundToEnd());
        }

        /// <summary>
        /// 等待声音播放结束的协程
        /// 当音频停止播放时，将该声音发射器返回到对象池中
        /// </summary>
        /// <returns>等待协程</returns>
        private IEnumerator WaitForSoundToEnd()
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
            SoundManager.Instance.ReturnToPool(this);
        }

        /// <summary>
        /// 停止播放声音
        /// 停止当前播放协程并将音频源停止，然后将该声音发射器返回到对象池中
        /// </summary>
        public void Stop()
        {
            if (playingCoroutine != null)
            {
                StopCoroutine(playingCoroutine);
                playingCoroutine = null;
            }
            
            audioSource.Stop();
            SoundManager.Instance.ReturnToPool(this);
        }
        
        /// <summary>
        /// 初始化声音发射器
        /// 根据提供的声音数据配置音频源的各种属性
        /// </summary>
        /// <param name="data">声音数据，包含音频剪辑、混音组、音量等配置信息</param>
        public void Initialize(SoundData data)
        {
            Data = data;
            audioSource.clip = data.clip;
            audioSource.outputAudioMixerGroup = data.mixerGroup;
            audioSource.loop = data.loop;
            audioSource.playOnAwake = data.playOnAwake;
            
            audioSource.mute = data.mute;
            audioSource.bypassEffects = data.bypassEffects;
            audioSource.bypassListenerEffects = data.bypassListenerEffects;
            audioSource.bypassReverbZones = data.bypassReverbZones;
            
            audioSource.priority = data.priority;
            audioSource.volume = data.volume;
            audioSource.pitch = data.pitch;
            audioSource.panStereo = data.panStereo;
            audioSource.spatialBlend = data.spatialBlend;
            audioSource.reverbZoneMix = data.reverbZoneMix;
            audioSource.dopplerLevel = data.dopplerLevel;
            audioSource.spread = data.spread;
            
            audioSource.minDistance = data.minDistance;
            audioSource.maxDistance = data.maxDistance;
            
            audioSource.ignoreListenerVolume = data.ignoreListenerVolume;
            audioSource.ignoreListenerPause = data.ignoreListenerPause;
            
            audioSource.rolloffMode = data.rolloffMode;
        }

        /// <summary>
        /// 为音频源添加随机音调变化
        /// 在当前音调基础上增加一个随机值，用于创建更自然的声音效果
        /// </summary>
        /// <param name="min">随机音调变化的最小值，默认为-0.05f</param>
        /// <param name="max">随机音调变化的最大值，默认为0.05f</param>
        public void WithRandomPitch(float min = -0.05f, float max = 0.05f)
        {
            audioSource.pitch += Random.Range(min, max);
        }
    }
}
