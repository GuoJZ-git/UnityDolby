using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoMgr : MonoBehaviour
{
  static IList<VideoFile> videoList = new List<VideoFile>();

  static GameObject prefabVideo;
  static GameObject prefabControl;


  // Use this for initialization
  void Start()
  {
    prefabVideo = Resources.Load<GameObject>("Prefabs/Media/Video");
    prefabControl = Resources.Load<GameObject>("Prefabs/Media/UIVideoPanel");
  }

  // Update is called once per frame
  void Update()
  {

  }

  public static VideoFile CreateVideoPanel(string movFile, GameObject parent)
  {
    GameObject objVideo = Instantiate(prefabVideo, parent.transform);
    if (!string.IsNullOrEmpty(movFile))
      objVideo.name = movFile;

    if (objVideo != null)
      objVideo.SetActive(true);
    else
      Debug.LogError("");

    parent.SetActive(true);

    VideoFile theVideo = objVideo.GetComponentInChildren<VideoFile>();
    theVideo.InitSource(); // asume before play

    GameObject objController = Instantiate(prefabControl, parent.transform);
    UIVideoController ctrl = objController.GetComponent<UIVideoController>();
    ctrl.InitPlayer(theVideo);

    videoList.Add(theVideo);

    return theVideo;
  }

  //static UIVideoPanel ctrl;
  public static void Play(string movFile)
  {
   // VideoClip clip = AssetLoader.Load<VideoClip>(movFile);

 //   theVideo.Play(clip);

   // ctrl.PrepareForClip(clip);
   //ctrl.Play();
    //theVideo.Play(clip);

  }
  public static void PlayUrl(string movFile)
  {
 
   // theVideo.playUrl(movFile);

  }
  public static void Stop()
  {

  //  theVideo?.Stop();

  }
}
