using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicmanager : MonoBehaviour
{
    public AudioClip[] musicClips; // 所有可能的背景音乐数组
    public AudioSource audioSource;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        // 应用从开始界面设置的背景音乐，并播放
        audioSource.clip = musicClips[GameSettings.SelectedMusicIndex];
        audioSource.Play();
    }

}
