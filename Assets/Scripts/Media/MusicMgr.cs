using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMgr : MonoBehaviour
{
  public GameObject prefabMusic;
  private static MusicMgr instance;

  // Start is called before the first frame update
  void Start()
  {
    instance = this;
  }

  // Update is called once per frame
  void Update()
  {

  }

  IList<Music> objMusic = new List<Music>();

  Music CreateMusic()
  {
    GameObject obj = Instantiate<GameObject>(prefabMusic, transform);
    Music music = obj.GetComponent<Music>();
    objMusic.Add(music);
    return music;
  }

  /// <summary>
  /// Music button
  /// </summary>
  public static void PauseMusic()
  {
    foreach( Music obj in instance.objMusic)
    {     
      obj.Pause();
    }
    // instance.objMusic.Clear(); 
  }
  public static void ResumeMusic()
  {
    foreach (Music obj in instance.objMusic)
    {
      obj.Play();
    }
  }

  public static void StopMusic()
  {
    foreach (Music obj in instance.objMusic)
    {
      obj.Stop();
      Destroy(obj.gameObject);
    }

    instance.objMusic.Clear(); 
  }

  public static Music PlayDolby(string musicFile)
  {
    Music music = instance.CreateMusic();
    music.name = musicFile;
    music.PlayDolby(musicFile);

    return music;
  }


  public static Music PlayDolby(AudioClip audioClip)
  {
    Music music = instance.CreateMusic();
    music.name = audioClip.name;
    music.PlayDolby(audioClip);

    return music;
  }
  public static Music PlayMp3(string musicFile)
  {
    Music music = instance.CreateMusic();
    music.name = musicFile;
    music.PlayMp3(musicFile);

    return music ;
  }


  public static Music PlayMp3(AudioClip audioClip)
  {
    Music music = instance.CreateMusic();
    music.name = audioClip.name;
    music.PlayMp3(audioClip);

    return music;
  }
}
