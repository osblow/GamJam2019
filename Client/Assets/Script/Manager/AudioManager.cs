using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource m_bgmSource;
    public AudioSource m_soundSource;

    GameObject m_uiCamera;

    void Awake()
    {
        m_bgmSource = gameObject.AddComponent<AudioSource>();
        m_bgmSource.playOnAwake = false;
        m_soundSource = gameObject.AddComponent<AudioSource>();
        m_soundSource.playOnAwake = false;
        m_uiCamera = GameObject.Find("UICamera");

        Instance = this; //通过Sound._instance.方法调用
    }

    //播放全局音效
    public void PlaySound(AudioInfo audio)
    {
        //这里目标文件处在 Resources/Sounds/目标文件name
        AudioClip clip = Resources.Load<AudioClip>(audio.resName);
        m_soundSource.clip = clip;
        m_soundSource.Play();
        m_soundSource.loop = audio.loop;
    }

    public void StopSound()
    {
        m_soundSource.Stop();
    }

    //在GameObject上播音效
    public void PlaySoundByGO(AudioInfo audio,GameObject go)
    {
        AudioSource audios = Util.GetOrAddComponent<AudioSource>(go);
        //这里目标文件处在 Resources/Sounds/目标文件name
        AudioClip clip = Resources.Load<AudioClip>(audio.resName);
        audios.clip = clip;
        audios.loop = audio.loop;
        if (audios.isPlaying)
        {
            return;
        }
        audios.Play();
    }

    public void StopSoundByGO(GameObject go)
    {
        AudioSource audios = Util.GetOrAddComponent<AudioSource>(go);
        audios.Stop();
    }

    //如果当前有其他音频正在播放，停止当前音频，播放下一个
    public void PlayBGM(AudioInfo audio)
    {
        AudioClip clip = Resources.Load<AudioClip>(audio.resName);

        if (m_bgmSource.isPlaying)
        {
            m_bgmSource.Stop();
        }

        m_bgmSource.clip = clip;
        m_bgmSource.loop = audio.loop;
        m_bgmSource.volume = audio.volume;
        m_bgmSource.Play();
        
    }

}
