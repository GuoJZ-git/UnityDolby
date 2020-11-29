using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
  public GameObject objSound;
  private static SoundMgr instance;


  // Start is called before the first frame update
  void Start()
  {
    instance = this;

  }

  // Update is called once per frame
  void Update()
  {
    
  }

  Sound CreatSound()
  {
    GameObject obj = Instantiate<GameObject>(objSound, transform);
    return obj.GetComponent<Sound>();
  }

  public static void PlaySound(string clipFile )
  {
    AudioClip audioClip = AssetLoader.Load<AudioClip>(clipFile);
    PlaySound(audioClip );
  }

  public static void PlaySound(string clipFile, Vector3 pos)
  {
    AudioClip audioClip = AssetLoader.Load<AudioClip>(clipFile);
    PlaySound(audioClip, pos);
  }

  public static void PlaySound(AudioClip audioClip)
  {
    Sound sound = instance.CreatSound();
    sound.Play(audioClip);
  }
  public static void PlaySound(AudioClip audioClip, Vector3 pos)
  {
    Sound sound = instance.CreatSound();
    sound.Play(audioClip, pos);
  }
}
