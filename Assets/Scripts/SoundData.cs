using System;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    /// <summary>
    /// 音频数据配置类，用于存储和管理音频播放的各种参数设置
    /// 包含音频剪辑、混音组、播放控制、音效处理等属性配置
    /// </summary>
    [Serializable]
    public class SoundData
    {
        /// <summary>
        /// 音频剪辑文件，用于播放的具体音频资源
        /// </summary>
        public AudioClip clip;
        
        /// <summary>
        /// 音频混音组，用于将音频输出到指定的混音器组进行统一处理
        /// </summary>
        public AudioMixerGroup mixerGroup;
        
        /// <summary>
        /// 是否循环播放音频
        /// </summary>
        public bool loop;
        
        /// <summary>
        /// 是否在组件唤醒时自动播放音频
        /// </summary>
        public bool playOnAwake;
        
        /// <summary>
        /// 标识是否为频繁播放的声音，可用于性能优化判断
        /// </summary>
        public bool frequentSound;
        
        /// <summary>
        /// 是否静音该音频
        /// </summary>
        public bool mute;
        
        /// <summary>
        /// 是否绕过音频效果处理
        /// </summary>
        public bool bypassEffects;
        
        /// <summary>
        /// 是否绕过监听器效果处理
        /// </summary>
        public bool bypassListenerEffects;
        
        /// <summary>
        /// 是否绕过混响区域效果
        /// </summary>
        public bool bypassReverbZones;
        
        /// <summary>
        /// 音频优先级，数值范围通常为0-256，数值越高优先级越低
        /// </summary>
        public int priority = 128;
        
        /// <summary>
        /// 音频音量大小，取值范围0-1
        /// </summary>
        public float volume = 1f;
        
        /// <summary>
        /// 音频音调倍率，1为正常音调
        /// </summary>
        public float pitch = 1f;
        
        /// <summary>
        /// 立体声平衡，取值范围-1到1，-1为纯左声道，1为纯右声道
        /// </summary>
        public float panStereo;
        
        /// <summary>
        /// 3D立体声混合比例，0为2D音频，1为3D音频
        /// </summary>
        public float spatialBlend;
        
        /// <summary>
        /// 混响区域混合强度，控制音频受混响区域影响的程度
        /// </summary>
        public float reverbZoneMix = 1f;
        
        /// <summary>
        /// 多普勒效应等级，控制移动速度对音调的影响程度
        /// </summary>
        public float dopplerLevel = 1f;
        
        /// <summary>
        /// 立体声扩展角度，控制3D音频在立体声场中的扩散角度
        /// </summary>
        public float spread;
        
        /// <summary>
        /// 3D音频最小距离，在此距离内音量保持最大
        /// </summary>
        public float minDistance = 1f;
        
        /// <summary>
        /// 3D音频最大距离，在此距离外音频完全衰减
        /// </summary>
        public float maxDistance = 500f;
        
        /// <summary>
        /// 是否忽略监听器的音量设置
        /// </summary>
        public bool ignoreListenerVolume;
        
        /// <summary>
        /// 是否忽略监听器的暂停状态
        /// </summary>
        public bool ignoreListenerPause;
        
        /// <summary>
        /// 音频衰减模式，定义音频随距离变化的衰减曲线类型
        /// </summary>
        public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;
    }
}
