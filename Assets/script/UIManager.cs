// UIManager.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Toggle[] musicToggles; // 勾选框数组
    public AudioClip[] musicClips; // 所有可能的背景音乐数组

    void Start()
    {
        // 为每个Toggle添加事件监听器
        for (int i = 0; i < musicToggles.Length; i++)
        {
            int index = i;
            musicToggles[i].onValueChanged.AddListener(delegate {
                OnMusicToggleChange(index);
            });
        }
    }

    // Toggle状态变化时调用此方法
    void OnMusicToggleChange(int index)
    {
        if (musicToggles[index].isOn)
        {
            // 更新GameSettings中的静态变量，保存用户选择的音乐索引
            GameSettings.SelectedMusicIndex = index;

            // 假设你有一个数组或其它方式存储 AudioClip 引用
            AudioClip clipToSave = GetAudioClipFromIndex(index);

            // 保存音频数据
           // AudioSaver.Save("bgm.wav", clipToSave);
        }
    }

    // 应有一个方法从index中获取音频剪辑
    AudioClip GetAudioClipFromIndex(int index)
    {
        // 根据你的项目结构，实现从索引获取AudioClip的逻辑
        // 例如，你可能有一个AudioClip数组与toggles关联
        return musicClips[index];
    }


}