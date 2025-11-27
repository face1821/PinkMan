using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        // 单例模式，确保只有一个音乐管理器
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 关键：场景切换时不销毁

            audioSource = GetComponent<AudioSource>();
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // 如果已存在实例，销毁新创建的这个
            Destroy(gameObject);
        }
    }

    // 可选：提供控制方法
    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
