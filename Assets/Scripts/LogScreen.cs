using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;

//[System.Reflection.Obfuscation(Exclude = true)]
public class LogScreen : MonoBehaviour
{
    public int fontSize = 32;

    Vector2 vMousePos;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void RuntimeInitialize()
    {
        Application.logMessageReceivedThreaded += LogScreen.HandleLog;//;
     }

    private void Awake()
    {
      //  Application.logMessageReceivedThreaded += LogScreen.HandleLog;//;
    }

    // Use this for initialization
    void Start()
    {
        mLastFrameTime = TickToMilliSec(System.DateTime.Now.Ticks);
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    static ArrayList logInfo = new ArrayList();
  static ArrayList errInfo = new ArrayList();
  static ArrayList eInfo = new ArrayList();
  static ArrayList warnInfo = new ArrayList();
    static public void HandleLog(string logString, string stackTrace, LogType type)
    {
   
      switch ( type)
      {
        case LogType.Log:
        lock (logInfo)
          logInfo.Add(logString);
          break;
        case LogType.Error:
        lock(errInfo)
          errInfo.Add(logString);
          break;
      case LogType.Exception:
        lock (eInfo)
          eInfo.Add(logString);
        break;
      case LogType.Warning:
        lock (warnInfo)
          warnInfo.Add(logString);
        break;
    }
    }

    float fLogTime = 0;
    // Update is called once per frame
    void Update()
    {
 
     

        fLogTime += Time.deltaTime;
      lock (logInfo)
      {
          if (logInfo.Count == 0)
          {
              fLogTime = 0;
          }
          else
          {
              if (logInfo.Count * fLogTime > 10.0f)
              {
             //     debugInfo.RemoveAt(0);
            //      fLogTime *= 0.5f;
              }
          }
      }
    }
    static int begin = 0;

    void OnGUI()
    {
        UpdateTick();

        GUI.skin.label.fontSize = fontSize;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        float x = 0;
        float y = 0;
        GUI.Label(new Rect(x, y, 640, 40), "fps: " + mLastFps);
        y += 32;
        GUI.Label(new Rect(x, y, 640, 40), string.Format("Screen: {0} {1}", Screen.width, Screen.height));
        y += 32;
        GUI.Label(new Rect(x, y, 640, 40), string.Format("Mouse pos : {0:0}, {1:0}", Input.mousePosition.x,    Input.mousePosition.y));
    y += 32;
   // GUI.Label(new Rect(x, y, 1000, 100), TUIO.IPHelper.localIPAddressesString);

        

        y += 32;

        x = 80;
    //  y = 32;

    GUILayout.BeginArea(new Rect(10, y, 1024, Screen.height ));//
    const int V = 20;
    lock (logInfo)
    {
      int nCount = logInfo.Count;
      begin = nCount >= V ? nCount - V : 0;
      GUILayout.Label(logInfo.Count.ToString());
      y += 32;
      for (int i = begin; i < nCount; i++)
      {
        string str = logInfo[i].ToString();
        y += 32;
        GUILayout.Label(str);
      }          
    }
    lock (errInfo)
    {
      int nCount = errInfo.Count;
      begin = nCount >= V ? nCount - V : 0;
      GUILayout.Label(errInfo.Count.ToString());
      y += 32;
      for (int i = begin; i < nCount; i++)
      {
        string str = errInfo[i].ToString();
        y += 32;
        GUILayout.Label(str);
      }
    }
    lock (eInfo)
    {
      int nCount = eInfo.Count;
      begin = (nCount >= V ? nCount - V : 0);
      GUILayout.Label(eInfo.Count.ToString());
      y += 32;
      for (int i = begin; i < nCount; i++)
      {
        string str = eInfo[i].ToString();
        y += 32;
        GUILayout.Label(str);
      }
    }
    lock (warnInfo)
    {
      int nCount = warnInfo.Count;
      begin = (nCount >= V ? nCount - V : 0);
      GUILayout.Label(warnInfo.Count.ToString());
      y += 32;
      for (int i = begin; i < nCount; i++)
      {
        string str = warnInfo[i].ToString();
        y += 32;
        GUILayout.Label(str);
      }
    }
    GUILayout.EndArea();
    //if (IF_OSS.porgress != null)
    //{
    //  lock (IF_OSS.porgress)
    //  {
    //    if (IF_OSS.porgress.TotalBytes > 0)
    //    {
    //      Rect rect = new Rect(1500, 800 - 100, 400, 1000);
    //      GUI.Label(rect, string.Format(" - {0:N0}/{1:N0}", IF_OSS.porgress.TransferredBytes, IF_OSS.porgress.TotalBytes));

    //      rect.yMin += 50;
    //      GUI.Label(rect, string.Format(" - {0:N0}%", IF_OSS.porgress.PercentDone));
    //      rect.yMin += 50;
    //      GUI.Label(rect, string.Format(" - 上传速度:{0:N0} KB/s", IF_OSS.porgress.TransferredBytes / OssUpload.fTimeUpload / 1000));
    //      rect.yMin += 50;
    //      GUI.Label(rect, string.Format(" - TransferTime:{0:N2} ", OssUpload.fTimeUpload));
    //    }
    //  }
    //}

    //       for (int i = 0; i < keypress.Length; i++)
    //          GUI.Label(new Rect(320, 264 + 32 * i, Screen.width / 3, Screen.height / 3),
    //             "Key " + keypress[i] );

  }

    private void DrawFps()
    {
        //         if (mLastFps > 50)
        //         {
        //             GUI.color = new Color(0, 1, 0);
        //         }
        //         else if (mLastFps > 40)
        //         {
        //             GUI.color = new Color(1, 1, 0);
        //         }
        //         else
        //         {
        //             GUI.color = new Color(1.0f, 0, 0);
        //         }

        GUI.Label(new Rect(50, 32, 2640, 24), "fps: " + mLastFps);

    }

    private long mFrameCount = 0;
    private long mLastFrameTime = 0;
    static long mLastFps = 0;
    private void UpdateTick()
    {
        mFrameCount++;
        long nCurTime = TickToMilliSec(System.DateTime.Now.Ticks);

        if ((nCurTime - mLastFrameTime) >= 1000)
        {
            long fps = (long)(mFrameCount * 1.0f / ((nCurTime - mLastFrameTime) / 1000.0f));

            mLastFps = fps;

            mFrameCount = 0;

            mLastFrameTime = nCurTime;

        }
    }

    public static long TickToMilliSec(long tick)
    {
        return tick / (10 * 1000);
    }

}