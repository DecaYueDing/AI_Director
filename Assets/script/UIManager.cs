// UIManager.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Toggle[] musicToggles; // ��ѡ������
    public AudioClip[] musicClips; // ���п��ܵı�����������

    void Start()
    {
        // Ϊÿ��Toggle����¼�������
        for (int i = 0; i < musicToggles.Length; i++)
        {
            int index = i;
            musicToggles[i].onValueChanged.AddListener(delegate {
                OnMusicToggleChange(index);
            });
        }
    }

    // Toggle״̬�仯ʱ���ô˷���
    void OnMusicToggleChange(int index)
    {
        if (musicToggles[index].isOn)
        {
            // ����GameSettings�еľ�̬�����������û�ѡ�����������
            GameSettings.SelectedMusicIndex = index;

            // ��������һ�������������ʽ�洢 AudioClip ����
            AudioClip clipToSave = GetAudioClipFromIndex(index);

            // ������Ƶ����
           // AudioSaver.Save("bgm.wav", clipToSave);
        }
    }

    // Ӧ��һ��������index�л�ȡ��Ƶ����
    AudioClip GetAudioClipFromIndex(int index)
    {
        // ���������Ŀ�ṹ��ʵ�ִ�������ȡAudioClip���߼�
        // ���磬�������һ��AudioClip������toggles����
        return musicClips[index];
    }


}