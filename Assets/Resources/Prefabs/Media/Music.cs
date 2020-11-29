using UnityEngine;

namespace Audio
{

  public class Music : MonoBehaviour
  {

    public AudioSource audioSource;

    bool bMusicPaused = false;

    // Use this for initialization
    void Start()
    {
      if (audioSource == null)
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //暂停播放
    public void Pause()
    {
      if (bMusicPaused == false)
      {
        bMusicPaused = true;

        audioSource.Pause();
      }
      else
      {
        bMusicPaused = false;

        audioSource.Play();
      }
    }


    //设置音量为0，播放中
    public void Mute(bool mute)
    {
      audioSource.mute = mute;
    }

    //开始播放。如果暂停，继续播放
    public void Play()
    {
      audioSource.Play();
    }

    // 停止播放
    public void Stop()
    {
      audioSource.Stop();
    }
     

    public void PlayDolby(string musicFile)
    {
      AudioClip clipMusic = AssetLoader.Load<AudioClip>(musicFile);
      PlayDolby(clipMusic);
    }

    public void PlayDolby(AudioClip clipMusic)
    {
      audioSource.clip = clipMusic;

      audioSource.minDistance = 1.0f;
      audioSource.maxDistance = 500.0f;
      audioSource.loop = false;
      audioSource.rolloffMode = AudioRolloffMode.Linear;
      audioSource.spatialize = true;
      audioSource.spatialBlend = 0.0f;

      Play();
    }

    public void PlayMp3(string musicFile)
    {
      AudioClip clipMusic = AssetLoader.Load<AudioClip>(musicFile);
      PlayMp3(clipMusic);
    }

    public void PlayMp3(AudioClip clipMusic)
    {
      audioSource.clip = clipMusic;

      audioSource.minDistance = 1.0f;
      audioSource.maxDistance = 500.0f;
      audioSource.loop = false;
      audioSource.rolloffMode = AudioRolloffMode.Linear;
      audioSource.spatialize = true;
      audioSource.spatialBlend = 1.0f;

      Play();
    }

    public void setPos(Vector3 pos)
    {
      audioSource.transform.position = pos;
      audioSource.spatialBlend = 1.0f;

    }
  }
}