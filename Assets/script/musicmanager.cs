using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicmanager : MonoBehaviour
{
    public AudioClip[] musicClips; // ���п��ܵı�����������
    public AudioSource audioSource;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        // Ӧ�ôӿ�ʼ�������õı������֣�������
        audioSource.clip = musicClips[GameSettings.SelectedMusicIndex];
        audioSource.Play();
    }

}
