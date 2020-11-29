using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

//[System.Reflection.Obfuscation(Exclude = true)]
public class DebugScreen : MonoBehaviour
{
    KeyCode[] keypress = new KeyCode[40];
    int nKey = 0;
    public int fontSize = 16;

    Vector2 vMousePos;

    private void Awake()
    {
        Application.logMessageReceivedThreaded += DebugScreen.HandleLog;//;
    }

    // Use this for initialization
    void Start()
    {
        mLastFrameTime = TickToMilliSec(System.DateTime.Now.Ticks);
    }

    static ArrayList debugInfo = new ArrayList();
    static public void HandleLog(string logString, string stackTrace, LogType type)
    {
        lock (debugInfo)
        {
            if (debugInfo.Count > 10)
                debugInfo.Remove(0);
            debugInfo.Add(logString);
        }
    }

    float fLogTime = 0;
    // Update is called once per frame
    void Update()
    {
        nKey = 0;

        //  AndroidCall.GetMemoryInfo();
        //    AndroidCall.getWIFIState();

        for (KeyCode key = KeyCode.None; key < (KeyCode)429; key++)
        {
            if (Input.GetKey(key))
            {
                keypress[nKey++] = key;
            }
        }

        fLogTime += Time.deltaTime;
        lock (debugInfo)
        {
            if (debugInfo.Count == 0)
            {
                fLogTime = 0;
            }
            else
            {
                if (debugInfo.Count * fLogTime > 10.0f)
                {
                    debugInfo.RemoveAt(0);
                    fLogTime *= 0.5f;
                }
            }
        }
    }

    void OnGUI()
    {
        UpdateTick();
        //   DrawFps();
        GUI.skin.label.fontSize = fontSize;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        float x = 10;// Screen.width * 1 / 2;
        float y = 160;
        GUI.Label(new Rect(x, y, 640, 40), "fps: " + mLastFps);
        y += 32;
        GUI.Label(new Rect(x, y, 640, 40), string.Format("Mouse pos : {0}, {1}", Input.mousePosition.x,
       Input.mousePosition.y));
        //     GUI.Label(new Rect(x+300, y, 640, 40), "BrainServer.ServerMgr.bClientAlive : "+ BrainServer.ServerMgr.bClientAlive );
        y += 32;

        lock (debugInfo)
        {  
            foreach (string str in debugInfo)
            {
                y += 32;
                GUI.Label(new Rect(x, y, 1900, 200), str);
            }

        }
        //          for (int i = 0; i < keypress.Length; i++)
        //             GUI.Label(new Rect(32, 564 + 16 * i, Screen.width / 3, Screen.height / 3),
        //                 "Key " + keypress[i] );
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


    public static string GetTotalMemory()
    {
#if UNITY_ANDROID
       try
        {
            AndroidJavaObject fileReader = new AndroidJavaObject("java.io.FileReader", "/proc/meminfo");
            AndroidJavaObject br = new AndroidJavaObject("java.io.BufferedReader", fileReader, 2048);
            string mline = br.Call<string>("readLine");

            br.Call("close");

            mline = mline.Substring(mline.IndexOf("MemTotal:"));
            mline = Regex.Match(mline, "(\\d+)").Groups[1].Value;

            return (int.Parse(mline) / 1000).ToString();
        }
        catch (Exception e)
        {
            return SystemInfo.systemMemorySize.ToString();
        }
#endif

        return "";
    }

}