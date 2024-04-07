using UnityEngine;
using UnityEngine.SceneManagement; 

/// <summary>
/// AudioCompletionLoader 类用于检测音频是否播放完毕，并在音频播放完毕后加载新的场景。
/// </summary>
public class AudioCompletionLoader : MonoBehaviour
{
    /// <summary>
    /// AudioSource 类型的公共变量，用于播放音频。
    /// </summary>
    public AudioSource audioSource; 

    /// <summary>
    /// 在每一帧更新时调用此方法。
    /// </summary>
    void Update()
    {
        // 如果 audioSource 不为空，且 audioSource 当前没有播放任何音频，且 audioSource.clip 不为空
        if (audioSource != null && !audioSource.isPlaying && audioSource.clip != null)
        {
            // 获取音频剪辑的长度
            float clipLength = audioSource.clip.length;
            // 获取音频当前播放的时间
            float currentTime = audioSource.time;

            // 如果当前播放的时间大于等于音频剪辑的长度
            if (currentTime >= clipLength)
            {
                // 加载场景索引为0的场景
                SceneManager.LoadScene(0);
            }
        }
    }
}