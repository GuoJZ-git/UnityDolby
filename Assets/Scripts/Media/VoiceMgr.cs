using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceMgr : MonoBehaviour
{
  public GameObject objVoice;
  private static VoiceMgr instance;

  // Start is called before the first frame update
  void Start()
  {
    instance = this;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void PauseVoice()
  {
    //  voice.Pause();
  }

  Voice CreatVoice()
  {
    GameObject obj = Instantiate<GameObject>(objVoice, transform);
    return obj.GetComponent<Voice>();
  }

  public static void PlayVoice(string voiceFile, Vector3 pos)
  {
    AudioClip audioClip = AssetLoader.Load<AudioClip>(voiceFile);
    PlayVoice(audioClip, pos);
  }

  public static void PlayVoice(AudioClip audioClip, Vector3 pos)
  {
    Voice voice = instance.CreatVoice();
    voice.play(audioClip, pos);
  }
}
