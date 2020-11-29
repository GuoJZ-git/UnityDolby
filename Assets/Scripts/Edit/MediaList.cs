using Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;
using XmlFile;



public class MediaList : MonoBehaviour
{
  public GameObject tipText;
  public GameObject objEditPanel;

  public enum LIST_TYPE
  {
    List_None,
    video_Inner,
    video_Url,
    video_Streaming,

    audio_Inner,
    audio_Url,
    audio_Streaming,
  };

  static LIST_TYPE listType = LIST_TYPE.List_None;

  struct MEDIA
  {
    public List<string> lisVideoRes;
    public List<string> lisVideoStreaming;
    public List<string> lisVideoUrl;
  };
  static MEDIA video;
  static MEDIA audio;

  public static MediaList Instance { get; set; }

  // Use this for initialization
  void Start()
  {
    Instance = this;

    string mp4Media = @Application.dataPath + @"/Media";
    video.lisVideoRes = GetInnerFileList<VideoClip>(mp4Media);
    string mp4Stream = @Application.dataPath + @"/StreamingAssets";
    video.lisVideoStreaming = GetFileList(mp4Stream, "*.mp4");
    string mp4Path = @System.Environment.CurrentDirectory + @"/Media";
    video.lisVideoUrl = GetFileList(mp4Path, "*.mp4");

    string mp3Media = @Application.dataPath + @"/Media";
    audio.lisVideoRes = GetInnerFileList<AudioClip>(mp3Media);
    string mp3Stream = @Application.dataPath + @"/StreamingAssets";
    audio.lisVideoStreaming = GetFileList(mp3Stream, "*.wav");
    string mp3Path = @System.Environment.CurrentDirectory;
    audio.lisVideoUrl = GetFileList(mp3Path, "*.wav");
  }

  // Update is called once per frame
  void Update()
  {

    if (Input.GetKeyUp(KeyCode.Escape))
    {
      if (objEditPanel.activeInHierarchy)
        objEditPanel.SetActive(false);
      // else
      //   objEditPanel.SetActive(true);
    }
    if (Input.GetMouseButtonDown(1))
    {
      objEditPanel.SetActive(false);
    }

    tipText.transform.position = Input.mousePosition;
    tipText.GetComponent<Text>().text = Input.mousePosition.ToString();


    if (Input.GetKeyDown(KeyCode.F2))
    {
      XmlStage.curStage.SaveXML();
    }
  }


  public static void Clear()
  {
    EditorPanel.Clear();
  }

  void updateXmlEditor()
  {


  }


  public static List<string> GetInnerFileList<T>(string path) where T : UnityEngine.Object
  {//路径  
    string fullPath = path;// "Assets/Media" + "/";

    string assetBundlespath = Application.dataPath + "/Media";
    T[] bundle = Resources.LoadAll<T>("Media");// l.LoadFromFile(assetBundlespath + "/mat.assetbundle");
                                               //AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlespath + "/AssetBundles");


    if (bundle.Length > 0)
    {
      List<string> list = new List<string>();

      //  string[] assetNames = assetBundle.GetAllAssetNames();
      for (int i = 0; i < bundle.Length; i++)
      {
        list.Add(bundle[i].name);
      }
      return list;
    }
    return null;
    //获取指定路径下面的所有资源文件  
    if (Directory.Exists(fullPath))
    {
      List<string> list = new List<string>();

      DirectoryInfo direction = new DirectoryInfo(fullPath);
      FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

      Debug.Log(files.Length);

      for (int i = 0; i < files.Length; i++)
      {
        if (files[i].Name.EndsWith(".meta"))
        {
          continue;
        }
        Debug.Log("Name:" + files[i].Name);
        list.Add(files[i].Name);

        return list;
        //Debug.Log( "FullName:" + files[i].FullName );  
        //Debug.Log( "DirectoryName:" + files[i].DirectoryName );  
      }
    }

    return null;
  }

  /// <summary>
  /// 获取指定文件夹下的所有文件
  /// </summary>
  /// <param name="path">文件夹路径</param>
  /// <returns>List<String></returns>
  public static List<string> GetFileList(string path, string pattern)
  {
    List<string> list = new List<string>();
    try
    {
      string[] arr = Directory.GetFileSystemEntries(path, pattern, SearchOption.AllDirectories);
      for (int i = 0; i < arr.Length; i++)
      {
        if (File.GetAttributes(arr[i]).CompareTo(FileAttributes.Directory) > 0)
        {
          list.Add(arr[i]);
        }
      }
    }
    catch (Exception)
    {
    }
    return list;
  }

  public static void ShowList(LIST_TYPE newType)
  {
    listType = newType;
    Instance.objEditPanel.SetActive(true);

    int nCount = 0;
    switch (listType)
    {
      case LIST_TYPE.audio_Inner:
        nCount = audio.lisVideoRes.Count;
        foreach (string mediaName in audio.lisVideoRes)
        {
          Button button = EditorPanel.AddItem(mediaName);
          button.onClick.AddListener(() =>
          {
            EventMgr.startEvent("显示关闭按钮");

            GameObject objLayer = UIMgr.CreateLayer(10);
            if (objLayer != null)
            {
              EditorPanel.Instance.gameObject.SetActive(false);

              AudioClip clipMp3 = Resources.Load<AudioClip>("Media/" + mediaName);

              Music music = MusicMgr.PlayMp3(clipMp3);
              music.gameObject.AddComponent<_Astroid>();
            }
          });
        }

        break;

      case LIST_TYPE.audio_Url:
        nCount = audio.lisVideoUrl.Count;
        foreach (string mediaName in audio.lisVideoUrl)
        {
          Button button = EditorPanel.AddItem(mediaName);
          button.onClick.AddListener(() =>
          {
            EventMgr.startEvent("显示关闭按钮");

            GameObject objLayer = UIMgr.CreateLayer(10);
            if (objLayer != null)
            {
              EditorPanel.Instance.gameObject.SetActive(false);
              VideoFile theVideo = VideoMgr.CreateVideoPanel("movie", objLayer);

              theVideo.playUrl(mediaName);
            }
          });
        }
        break;
      case LIST_TYPE.audio_Streaming:
        nCount = audio.lisVideoStreaming.Count;
        foreach (string mediaName in audio.lisVideoStreaming)
        {
          Button button = EditorPanel.AddItem(mediaName);
          button.onClick.AddListener(() =>
          {
            EventMgr.startEvent("显示关闭按钮");

            GameObject objLayer = UIMgr.CreateLayer(10);
            if (objLayer != null)
            {
              EditorPanel.Instance.gameObject.SetActive(false);
              Instance. StartCoroutine(Instance. LoadMusic(mediaName));
            //  VideoFile theVideo = VideoMgr.CreateVideoPanel("movie", objLayer);

            //  theVideo.playUrl(mediaName);
            }
          });
        }
        break;
      case LIST_TYPE.video_Inner:
        nCount = video.lisVideoRes.Count;
        foreach (string mediaName in video.lisVideoRes)
        {
          Button button = EditorPanel.AddItem(mediaName);
          button.onClick.AddListener(() =>
          {
            EventMgr.startEvent("显示关闭按钮");

            GameObject objLayer = UIMgr.CreateLayer(10);
            if (objLayer != null)
            {
              EditorPanel.Instance.gameObject.SetActive(false);
              VideoFile theVideo = VideoMgr.CreateVideoPanel("movie", objLayer);

              VideoClip videoClip = Resources.Load<VideoClip>("Media/" + mediaName);
              theVideo.Play(videoClip);
            }
          });
        }

        break;

      case LIST_TYPE.video_Url:
        nCount = video.lisVideoUrl.Count;
        foreach (string mediaName in video.lisVideoUrl)
        {
          Button button = EditorPanel.AddItem(mediaName);
          button.onClick.AddListener(() =>
          {
            EventMgr.startEvent("显示关闭按钮");

            GameObject objLayer = UIMgr.CreateLayer(10);
            if (objLayer != null)
            {
              EditorPanel.Instance.gameObject.SetActive(false);
              VideoFile theVideo = VideoMgr.CreateVideoPanel("movie", objLayer);

              theVideo.playUrl(mediaName);
            }
          });
        }
        break;
      case LIST_TYPE.video_Streaming:
        nCount = video.lisVideoStreaming.Count;
        foreach (string mediaName in video.lisVideoStreaming)
        {
          Button button = EditorPanel.AddItem(mediaName);
          button.onClick.AddListener(() =>
          {
            EventMgr.startEvent("显示关闭按钮");

            GameObject objLayer = UIMgr.CreateLayer(10);
            if (objLayer != null)
            {
              EditorPanel.Instance.gameObject.SetActive(false);
              VideoFile theVideo = VideoMgr.CreateVideoPanel("movie", objLayer);

              theVideo.playUrl(mediaName);
            }
          });
        }
        break;
      case LIST_TYPE.List_None:
        break;
    }
    Vector2 sz = EditorPanel.Instance.parent.GetComponent<RectTransform>().sizeDelta;
    sz.y += 20;
    sz.y = nCount * 64;
    EditorPanel.Instance.parent.GetComponent<RectTransform>().sizeDelta = sz;

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
