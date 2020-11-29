using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoFile : MonoBehaviour
{
  [SerializeField]
  private bool startAfterPreparation = true;

  [Header("Optional")]

  [SerializeField]
  private VideoPlayer videoPlayer;

  [SerializeField]
  private AudioSource audioSource;

  [Header("Events")]

  [SerializeField]
  private UnityEvent onPrepared = new UnityEvent();

  [SerializeField]
  private UnityEvent onStartedPlaying = new UnityEvent();

  [SerializeField]
  private UnityEvent onFinishedPlaying = new UnityEvent();

  #region Properties

  public bool StartAfterPreparation
  {
    get { return startAfterPreparation; }
    set { startAfterPreparation = value; }
  }

  public UnityEvent OnPrepared
  {
    get { return onPrepared; }
  }

  public UnityEvent OnStartedPlaying
  {
    get { return onStartedPlaying; }
  }

  public UnityEvent OnFinishedPlaying
  {
    get { return onFinishedPlaying; }
  }

  public ulong Time
  {
    get { return (ulong)videoPlayer.time; }
  }

  public bool IsPlaying
  {
    get { return videoPlayer.isPlaying; }
  }

  public bool IsPrepared
  {
    get { return videoPlayer.isPrepared; }
  }

  public float NormalizedTime
  {
    get { return (float)(videoPlayer.time / Duration); }
  }

  public ulong Duration
  {
    get { return videoPlayer.frameCount / (ulong)videoPlayer.frameRate; }
  }

  public float Volume
  {
    get { return audioSource == null ? videoPlayer.GetDirectAudioVolume(0) : audioSource.volume; }
    set
    {
      if (audioSource == null)
        videoPlayer.SetDirectAudioVolume(0, value);
      else
        audioSource.volume = value;
    }
  }
  #endregion
  public VideoPlayer getVideoPlayer() { return videoPlayer; }
  public AudioSource getAudioSource() { return audioSource; }

  // Start is called before the first frame update
  void Start()
   {
     videoPlayer= GetComponent<VideoPlayer>();
     audioSource= GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
    {
        
    }
  private void OnEnable()
  {
    SubscribeToVideoPlayerEvents();
  }

  private void OnDisable()
  {
    UnsubscribeFromVideoPlayerEvents();
  }

  public void InitSource()
  {
   // videoPlayer = GetComponent<VideoPlayer>();
   // audioSource = GetComponent<AudioSource>();

    SubscribeToVideoPlayerEvents();
  }

  public void Play(UnityEngine.Video.VideoClip videoClip)
  {
  //  videoPlayer.Stop();

    RenderTexture.active = videoPlayer.targetTexture;
    GL.Clear(false, true, new Color(0.0f, 0.0f, 1.0f, 1.00f));
    GL.Color(new Color(0.0f, 0.0f, 1.0f, 1.0f));
     RenderTexture.active = null;

    videoPlayer.source = VideoSource.VideoClip;

    videoPlayer.clip = videoClip;

    videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
    Debug.Log("videoClip.audioTrackCount= " + videoClip.audioTrackCount);
    videoPlayer.EnableAudioTrack(0, true);
    videoPlayer.SetTargetAudioSource(0, audioSource);
    videoPlayer.prepareCompleted += (source) =>
    {
      source.Play();
      audioSource.Play();
    };

    videoPlayer.loopPointReached += (source) =>
    {
      EventMgr.startEvent("关闭");
    };

    videoPlayer.Prepare();
  }

  public void playUrl(string strUrl)
  {
    // videoPlayer.Stop();
    RenderTexture.active = videoPlayer.targetTexture;
    GL.Clear(false, true, new Color(0.0f, 0.0f, 0.0f, 1.00f));
    RenderTexture.active = null;

    videoPlayer.playOnAwake = false;
    audioSource.playOnAwake = false;

    videoPlayer.source = VideoSource.Url;
    videoPlayer.url = strUrl;

    videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
    ////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    videoPlayer.controlledAudioTrackCount = 1;///
    ///////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    videoPlayer.EnableAudioTrack(0, true);
    videoPlayer.SetTargetAudioSource(0, audioSource);

    videoPlayer.prepareCompleted += (source) =>
    {

       Debug.Log("Clip: " + source.name);
       Debug.Log("frameCount: " + source.frameCount.ToString());
       Debug.Log("frameRate: " + source.frameRate.ToString());
       Debug.Log("length: " + source.length.ToString());
       Debug.Log("width: " + source.width.ToString());
       Debug.Log("height: " + source.height.ToString());
       Debug.Log("pixelAspectRatioNumerator: " + source.pixelAspectRatioNumerator.ToString());
       Debug.Log("pixelAspectRatioDenominator: " + source.pixelAspectRatioDenominator.ToString());
       Debug.Log("audioTrackCount: " + source.audioTrackCount.ToString());
       Debug.Log("controlledAudioTrackCount: " + source.controlledAudioTrackCount.ToString());
       Debug.Log("AudioChannelCount: " + source.GetAudioChannelCount(0).ToString());


      source.Play();
      audioSource.Play();
    };

    videoPlayer.loopPointReached += (source) =>
    {
      EventMgr.startEvent("关闭");
    };
  
   
      videoPlayer.Prepare();
    
  }
  
  public  void Stop()
  {
    if (videoPlayer != null)
    {
      videoPlayer.Stop();
      RenderTexture temp = RenderTexture.GetTemporary(1920, 1080);
      RenderTexture.ReleaseTemporary(temp);
    }
  }


  #region Public Methods

  public void PrepareForUrl(string url)
  {
    if (videoPlayer != null)
    {
      videoPlayer.source = VideoSource.Url;
      videoPlayer.url = url;
      videoPlayer.Prepare();
    }
  }

  public void PrepareForClip(UnityEngine.Video.VideoClip clip)
  {
    if (videoPlayer != null)
    {
      videoPlayer.source = VideoSource.VideoClip;
      videoPlayer.clip = clip;
      videoPlayer.Prepare();
    }
  }

  public void Play()
  {
    if (!IsPrepared)
    {
      videoPlayer.Prepare();
      return;
    }

    videoPlayer.Play();
  }

  public void Pause()
  {
    videoPlayer.Pause();
  }

  public void TogglePlayPause()
  {
    if (IsPlaying)
      Pause();
    else
      Play();
  }

  public void Seek(float time)
  {
    time = Mathf.Clamp(time, 0, 1);
    videoPlayer.time = time * Duration;
  }
  #endregion
  #region Private Methods

  private void OnPrepareCompleted(VideoPlayer source)
  {
    onPrepared.Invoke();
    SetupAudio();

    if (StartAfterPreparation)
      Play();
  }

  private void OnStarted(VideoPlayer source)
  {
    onStartedPlaying.Invoke();
  }

  private void OnFinished(VideoPlayer source)
  {
    onFinishedPlaying.Invoke();
  }

  private void OnError(VideoPlayer source, string message)
  {
    Debug.LogError("OnError " + message);
  }

  private void SetupAudio()
  {
    if (videoPlayer.audioTrackCount <= 0)
      return;

    if (audioSource == null && videoPlayer.canSetDirectAudioVolume)
    {
      videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
    }
    else
    {
      videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
      videoPlayer.SetTargetAudioSource(0, audioSource);
    }
    videoPlayer.controlledAudioTrackCount = 1;
    videoPlayer.EnableAudioTrack(0, true);
  }

  private void SubscribeToVideoPlayerEvents()
  {
    if (videoPlayer == null)
      return;

    videoPlayer.errorReceived += OnError;
    videoPlayer.prepareCompleted += OnPrepareCompleted;
    videoPlayer.started += OnStarted;
    videoPlayer.loopPointReached += OnFinished;
  }

  private void UnsubscribeFromVideoPlayerEvents()
  {
    if (videoPlayer == null)
      return;

    videoPlayer.errorReceived -= OnError;
    videoPlayer.prepareCompleted -= OnPrepareCompleted;
    videoPlayer.started -= OnStarted;
    videoPlayer.loopPointReached -= OnFinished;
  }
  #endregion

}
