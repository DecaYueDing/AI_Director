// GameSceneManager.cs
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public AudioClip[] musicClips; // ���п��ܵı�����������
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Ӧ�ôӿ�ʼ�������õı������֣�������
        audioSource.clip = musicClips[GameSettings.SelectedMusicIndex];
        audioSource.Play();
    }
}