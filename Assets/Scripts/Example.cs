using Action;
using Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using XmlFile;

public class Example : MonoBehaviour
{
  public AudioClip clipBird;
  public AudioClip clipMp3;
  public AudioClip clipDolby;

  public GameObject objShow;

  float distance = 160f;

  public Button buttonPlay_Voice_FL;
  public Button buttonPlay_Voice_FC;
  public Button buttonPlay_Voice_FR;
  public Button buttonPlay_Voice_CL;
  public Button buttonPlay_Voice_CC;
  public Button buttonPlay_Voice_CR;
  public Button buttonPlay_Voice_RL;
  public Button buttonPlay_Voice_RC;
  public Button buttonPlay_Voice_RR;


  public Button buttonPlay_voice_FL_FR;
  public Button buttonPlay_voice_FL_RR;
  public Button buttonPlay_voice_FL_RL;
  public Button buttonPlay_voice_FR_FL;
  public Button buttonPlay_voice_FR_RL;
  public Button buttonPlay_voice_FR_RR;

  public Button buttonPlay_voice_RL_FL;
  public Button buttonPlay_voice_RL_FR;
  public Button buttonPlay_voice_RL_RR;
  public Button buttonPlay_voice_RR_RL;
  public Button buttonPlay_voice_RR_FL;
  public Button buttonPlay_voice_RR_FR;

  public Button buttonPlay_voice_L2R;
  public Button buttonPlay_voice_R2L;
  public Button buttonPlay_voice_F2R;
  public Button buttonPlay_voice_R2F;


  public Button buttonPlay_movie_inner;
  public Button buttonPlay_movie_url;
  public Button buttonPlay_movie_StreamingAssets;

  public Button buttonPlay_music_mp3;
  public Button buttonPlay_music_dts;
  public Button buttonStop_music;

  public Button buttonPlay_sound;
  public Button buttonPlay_bird;

  struct SoundPos
    {
  internal  static Vector3 _FL = new Vector3(-100, 0, 100);
  internal static Vector3 _FC = new Vector3(0, 0, 100);
  internal static Vector3 _FR = new Vector3(100, 0, 100);
  internal static Vector3 _CL = new Vector3(-100, 0, 0);
  internal static Vector3 _CC = new Vector3(0, 0, 0);
  internal static Vector3 _CR = new Vector3(100, 0, 0);
  internal static Vector3 _RL = new Vector3(-100, 0, -100);
  internal static Vector3 _RC = new Vector3(0, 0, -100);
    internal static Vector3 _RR = new Vector3(100, 0, -100);
  };

  // Use this for initialization
  void Start()
  {

    objShow.SetActive(false);

    buttonPlay_Voice_FL.onClick.AddListener(() => { _play_voice_pos(SoundPos._FL); });
    buttonPlay_Voice_FC.onClick.AddListener(() => { _play_voice_pos(SoundPos._FC); });
    buttonPlay_Voice_FR.onClick.AddListener(() => { _play_voice_pos(SoundPos._FR); });
    buttonPlay_Voice_CL.onClick.AddListener(() => { _play_voice_pos(SoundPos._CL); });
    buttonPlay_Voice_CC.onClick.AddListener(() => { _play_voice_pos(SoundPos._CC); });
    buttonPlay_Voice_CR.onClick.AddListener(() => { _play_voice_pos(SoundPos._CR); });
    buttonPlay_Voice_RL.onClick.AddListener(() => { _play_voice_pos(SoundPos._RL); });
    buttonPlay_Voice_RC.onClick.AddListener(() => { _play_voice_pos(SoundPos._RC); });
    buttonPlay_Voice_RR.onClick.AddListener(() => { _play_voice_pos(SoundPos._RR); });

    buttonPlay_voice_FL_FR.onClick.AddListener(() => { _play_line(SoundPos._FL, SoundPos._FR); });
    buttonPlay_voice_FL_RR.onClick.AddListener(() => { _play_line(SoundPos._FL, SoundPos._RR); });
    buttonPlay_voice_FL_RL.onClick.AddListener(() => { _play_line(SoundPos._FL, SoundPos._RL); });
    buttonPlay_voice_FR_FL.onClick.AddListener(() => { _play_line(SoundPos._FR, SoundPos._FL); });
    buttonPlay_voice_FR_RL.onClick.AddListener(() => { _play_line(SoundPos._FR, SoundPos._RL); });
    buttonPlay_voice_FR_RR.onClick.AddListener(() => { _play_line(SoundPos._FR, SoundPos._RR); });
    buttonPlay_voice_RL_FL.onClick.AddListener(() => { _play_line(SoundPos._RL, SoundPos._FL); });
    buttonPlay_voice_RL_FR.onClick.AddListener(() => { _play_line(SoundPos._RL, SoundPos._FR); });
    buttonPlay_voice_RL_RR.onClick.AddListener(() => { _play_line(SoundPos._RL, SoundPos._RR); });
    buttonPlay_voice_RR_RL.onClick.AddListener(() => { _play_line(SoundPos._RR, SoundPos._RL); });
    buttonPlay_voice_RR_FL.onClick.AddListener(() => { _play_line(SoundPos._RR, SoundPos._FL); });
    buttonPlay_voice_RR_FR.onClick.AddListener(() => { _play_line(SoundPos._RR, SoundPos._FR); });


    buttonPlay_voice_L2R.onClick.AddListener(() => { _play_line(SoundPos._CL, SoundPos._CR); });
    buttonPlay_voice_R2L.onClick.AddListener(() => { _play_line(SoundPos._CR, SoundPos._CL); });
    buttonPlay_voice_F2R.onClick.AddListener(() => { _play_line(SoundPos._FC, SoundPos._FR); });
    buttonPlay_voice_R2F.onClick.AddListener(() => { _play_line(SoundPos._FR, SoundPos._FC); });


  buttonPlay_sound.onClick.AddListener(_play_sound);
     buttonPlay_movie_inner.onClick.AddListener(() => {   MediaList.ShowList(MediaList.LIST_TYPE.video_Inner); });
  //   buttonPlay_movie_inner.onClick.AddListener(() => { _play_movie_inner(); });// MediaList.ShowList(MediaList.LIST_TYPE.List_Inner); });
    buttonPlay_movie_url.onClick.AddListener(()=> { MediaList.ShowList(MediaList.LIST_TYPE.video_Url); });
    buttonPlay_movie_StreamingAssets.onClick.AddListener(() => { MediaList.ShowList(MediaList.LIST_TYPE.video_Streaming); });

    buttonPlay_music_mp3.onClick.AddListener(() =>
    {
      MediaList.ShowList(MediaList.LIST_TYPE.audio_Inner);
      //      Music music = _play_music_mp3();
      //    music.gameObject.AddComponent<_Astroid>();
    });

    buttonPlay_music_dts.onClick.AddListener(()=> { _play_music_dolby(); });
    buttonStop_music.onClick.AddListener(()=> {
      //EventMgr.startEvent("停止音乐");
      MediaList.ShowList(MediaList.LIST_TYPE.audio_Streaming);
//      StartCoroutine(LoadMusic(@"j:\_Projects\_Sound5.1\Sound5.1\Assets\Resources\Media\Title1 - Chapter 01.wav"));
      bBird = false;
    });

    buttonPlay_bird.onClick.AddListener(_bird);
  }

  // Update is called once per frame
  void Update()
  {
   }


  private void OnGUI()
  {
   
  }



  void _play_line(Vector3 from, Vector3 to )
  {
    Music music = _play_music_mp3();
    _Line line = music.gameObject.AddComponent<_Line>();
    line.InitLine(from, to);
  }


  void _play_voice_pos(Vector3 pos)
  {
    EventMgr.startEvent("显示关闭按钮");
 
    pos = Camera.main.transform.TransformPoint(pos);
    VoiceMgr.PlayVoice("f0", pos);
    // SoundMgr.PlaySound("f0", pos);
  }

  void _play_sound()
  {
    SoundMgr.PlaySound("f0");
    EventMgr.startEvent("显示关闭按钮");
  }

  void _play_movie_inner()
  {
    EventMgr.startEvent("显示关闭按钮");
    EventMgr.startEvent("播放视频");
  }

  void _play_movie_url()
  {
    EventMgr.startEvent("显示关闭按钮");

    GameObject objLayer = UIMgr.CreateLayer(10);
    if (objLayer != null)
    {
      VideoFile theVideo = VideoMgr.CreateVideoPanel("movie", objLayer);

      //  string movFile = System.Environment.CurrentDirectory + @"/姜子牙.mp4";
      //  string movFile = @"c:\迅雷下载\The.Eight.Hundred.2020.1080p.HDRip.x264.AC3-FEWAT.mp4";

      //VideoMgr.PlayUrl(@"h:\Movies\babes tracy lindsay and aiko may sexors.mp4");
      //  VideoMgr.PlayUrl(@"h:\Dolby\DOLBY.VISION.CES.March.2018.UHD.BDRemux.2160p.DV.IVA.ExKinoRay\02.Chameleon.mp4");
      // VideoMgr.PlayUrl("file://" + @Application.streamingAssetsPath + @"/姜子牙.mp4");

      string movFile = @System.Environment.CurrentDirectory + @"\Pinball-JDBBS.mp4";
      theVideo.playUrl(movFile);

      //EventMgr.startEvent("播放视频");
    }
  }


  void _play_movie_streamingAssets()
  {
    MediaList.ShowList( MediaList.LIST_TYPE.video_Streaming);
  // Editor. listType = LIST_TYPE.list_Streaming;
    return;
    //EventMgr.startEvent("显示关闭按钮");

    //GameObject objLayer = UIMgr.CreateLayer(10);
    //if (objLayer != null)
    //{
    //  Video theVideo = VideoMgr.CreateVideoPanel("movie", objLayer);

    //  //  string movFile = System.Environment.CurrentDirectory + @"/姜子牙.mp4";
    //  //  string movFile = @"c:\迅雷下载\The.Eight.Hundred.2020.1080p.HDRip.x264.AC3-FEWAT.mp4";

    //  //VideoMgr.PlayUrl(@"h:\Movies\babes tracy lindsay and aiko may sexors.mp4");
    //  //  VideoMgr.PlayUrl(@"h:\Dolby\DOLBY.VISION.CES.March.2018.UHD.BDRemux.2160p.DV.IVA.ExKinoRay\02.Chameleon.mp4");
    //  // VideoMgr.PlayUrl("file://" + @Application.streamingAssetsPath + @"/姜子牙.mp4");

    //  string movFile = "file://" + @Application.streamingAssetsPath + @"/姜子牙.mp4";
    //  theVideo.playUrl(movFile);

    //  //EventMgr.startEvent("播放视频");
    //}
  }

  void _bird()
  {
    if (bBird) bBird = false;
    else
    {
      bBird = true;

      EventMgr.startEvent("显示关闭按钮");
      StartCoroutine(bird());
    }
  }

  bool bBird = false;
  IEnumerator bird()
  {
    while (bBird)
    {
      float x0 = 400 * Mathf.Sin(UnityEngine.Random.Range(0, 6.28f));
      float z0 = 400 * Mathf.Cos(UnityEngine.Random.Range(0, 6.28f));

      SoundMgr.PlaySound(clipBird, new Vector3(x0, 0, z0));

      yield return new WaitForSeconds(UnityEngine.Random.Range(4, 8));
    }
  }

  void _stop_movie()
  {
    EventMgr.startEvent("停止视频");
  }


  Music _play_music_dolby()
  {
    //  objShow.SetActive(true);
    EventMgr.startEvent("显示关闭按钮");

    Music music = MusicMgr.PlayDolby(clipDolby);

    EventMgr.startEvent("显示关闭按钮");
    return music;
  }

  Music _play_music_mp3()
  {
    Music music = MusicMgr.PlayMp3(clipMp3);

    EventMgr.startEvent("显示关闭按钮");

    Debug.Log("播放音乐文件:" + clipMp3.name);

    return music;
  }

  Music _play_music_stream(string mediaName)
  {
    EventMgr.startEvent("显示关闭按钮");

    GameObject objLayer = UIMgr.CreateLayer(10);
    if (objLayer != null)
    {
      EditorPanel.Instance.gameObject.SetActive(false);
      StartCoroutine(LoadMusic(mediaName));
      //  VideoFile theVideo = VideoMgr.CreateVideoPanel("movie", objLayer);

      //  theVideo.playUrl(mediaName);
    }

    return null;
  }



  //filepath 绝对路径 安卓sdcard 或者PC 绝对路径
  public IEnumerator LoadMusic(string filepath)
  {
    filepath = "file://" + filepath;
    using (var uwr = UnityWebRequestMultimedia.GetAudioClip(filepath, AudioType.UNKNOWN))
    {
      //不卡顿的2行代码
      ((DownloadHandlerAudioClip)uwr.downloadHandler).compressed = false;
      ((DownloadHandlerAudioClip)uwr.downloadHandler).streamAudio = true;
      yield return uwr.SendWebRequest();
      if (uwr.isNetworkError)
      {
        Debug.LogError(uwr.error);
      }
      else
      {
        AudioClip clip = DownloadHandlerAudioClip.GetContent(uwr);

        MusicMgr.PlayDolby(clip);//播放
      }
    }
  }
}
