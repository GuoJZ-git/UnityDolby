using Audio;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMgr : MonoBehaviour
{
  struct VOLUME_SET
  {
    public float master;
    public float music;
    public float voice;
    public float sound;
  };

  static VOLUME_SET preset;
  static VOLUME_SET custom;

  public AudioMixer audioMixer;    // 进行控制的Mixer变量


  static AudioMgr instance;

  private void Start()
  {
    instance = this;
    custom.master = PlayerPrefs.GetFloat("MasterVolume");
    custom.music = PlayerPrefs.GetFloat("BGMVolume");
    custom.voice = PlayerPrefs.GetFloat("VoiceVolume");
    custom.sound = PlayerPrefs.GetFloat("SfxVolume");

    audioMixer.GetFloat("MasterVolume", out preset.master);
    audioMixer.GetFloat("BGMVolume", out preset.music);
    audioMixer.GetFloat("VoiceVolume", out preset.voice);
    audioMixer.GetFloat("SfxVolume", out preset.sound);

    audioMixer.SetFloat("MasterVolume", custom.master);
    audioMixer.SetFloat("BGMVolume", custom.music);
    audioMixer.SetFloat("VoiceVolume", custom.voice);
    audioMixer.SetFloat("SfxVolume", custom.sound);



  //  voice.audioSource.pitch = Time.timeScale;
    custom = preset;
  }

  private void Update()
  {
   // voice.audioSource.pitch = Time.timeScale;

  }

  private void OnGUI()
  {
    //GUI.skin.label.fontSize = 32;
    //GUI.Label( new Rect( 100, 100, 1000, 40), " Audio Pause = " + bAudioPaused);
    //GUI.Label(new Rect(100, 200, 1000, 40), " Music Pause = " + bMusicPaused);
  }

  private void OnApplicationQuit()
  {
    PlayerPrefs.SetFloat("MasterVolume", custom.master);
    PlayerPrefs.SetFloat("BGMVolume", custom.music);
    PlayerPrefs.SetFloat("VoiceVolume", custom.voice);
    PlayerPrefs.SetFloat("SfxVolume", custom.sound);
  }

  #region Volume control
  void SetMasterVolume(float volume)    // 控制主音量的函数
  {
    custom.master = volume;
    audioMixer.SetFloat("MasterVolume", custom.master);
    // MasterVolume为我们暴露出来的Master的参数
  }

  static void SetMusicVolume(float volume)    // 控制背景音乐音量的函数
  {
    custom.music = volume;
    instance.audioMixer.SetFloat("BGMVolume", custom.music);
    // MusicVolume为我们暴露出来的Music的参数
  }

  void SetVoiceVolume(float volume)    // 控制音效音量的函数
  {
    custom.voice = volume;
    audioMixer.SetFloat("VoiceVolume", custom.voice);
    // SoundEffectVolume为我们暴露出来的SoundEffect的参数
  }

  void SetSfxVolume(float volume)    // 控制音效音量的函数
  {
    custom.sound = volume;
    audioMixer.SetFloat("SfxVolume", custom.sound);
    // SoundEffectVolume为我们暴露出来的SoundEffect的参数
  }

  #endregion

  #region Mixer state
  public bool IsMasterOff
  {
    get { return custom.master < -70; }
  }
  public bool IsMusicOff
  {
    get { return custom.music < -70; }
  }
  public bool IsVoiceOff
  {
    get { return custom.voice < -70; }
  }
  #endregion



  #region music
  

  //开
  public static void TurnOnMusic()
  {
    custom.music = preset.music;

    MusicMgr.ResumeMusic();
     
    SetMusicVolume(custom.music);
  }

  //关
  public static void TurnOffMusic()
  {
    custom.music = -80;
    SetMusicVolume(custom.music);

    MusicMgr.PauseMusic();
  }


  //开关
  public bool ToggleMusic()
  {
    bool bMusicOn = false;

    if (custom.music < -70)
    {
      TurnOnMusic();
      TurnOnMaster();
      bMusicOn = true;
    }
    else
    {
      TurnOffMusic();
      if (IsVoiceOff)
        TurnOffMaster();
      bMusicOn = false;
    }

    return bMusicOn;
  }
 
  public void TurnOnVoice()
  {
    custom.voice = preset.voice;
    SetVoiceVolume(custom.voice);
  }

  public static void TurnOffVoice()
  {
    custom.voice = -80;
    instance.SetVoiceVolume(custom.voice);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns>
  /// mute:false
  /// </returns>
  public bool ToggleVoice()
  {
    bool bVoiceOn = false;
    if (custom.voice < -70)
    {
      TurnOnVoice();
      TurnOnMaster();
      bVoiceOn = true;
    }
    else
    {
      TurnOffVoice();
      if (IsMusicOff)
        TurnOffMaster();
      bVoiceOn = false;
    }

    return bVoiceOn;
  }
  #endregion


  #region master
  public void TurnOnMaster()
  {
    custom.master = preset.master;
    SetMasterVolume(custom.master);
  }

  public void TurnOffMaster()
  {
    custom.master = -80;
    SetMasterVolume(custom.master);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns>
  /// true= !Mute
  /// </returns>
  public bool ToggleMaster()
  {
    bool bMasterOn = false;// custom.master < -70;// (custom.music < -70 && custom.voice < -70);
                           //bMute = !bMute;
    if (custom.master < -70)
    {
      TurnOnMaster();
      TurnOnMusic();
      TurnOnVoice();
      bMasterOn = true;
    }
    else
    {
      TurnOffMaster();
      TurnOffMusic();
      TurnOffVoice();
      bMasterOn = false;
    }

    return bMasterOn;
  }

  #endregion
}
