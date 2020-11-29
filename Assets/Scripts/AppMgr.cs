using Action;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XmlFile;

public class AppMgr : MonoBehaviour
{

    void Main()
    {
        //   verifyFile();
    }

    void VerifyFile()
    {
        object[] objList = Resources.LoadAll(AssetLoader.xmlPath, typeof(TextAsset));
        foreach (object obj in objList)
        {
            TextAsset fileContent = obj as TextAsset;
            if (fileContent.name.Contains("stage"))
            {
                int nPage = int.Parse(fileContent.name.Substring(6, 2));

                string data = fileContent.text.ToString();

                XmlPage newStage = new XmlPage(nPage);

                newStage.ParseXML(data);

                newStage.SaveXML();
            }
        }
    }




    private void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {

       XmlStage. curStage = null;

    }
    private void LateUpdate()
    {
        //使用Unity 自动适配， 延迟载入
        if (XmlStage.curStage == null && step++ > 1)
        {
            if (step > 2)
                LoadPage(0);
        }

        if (XmlStage.curStage!=null)
        XmlStage.curStage.Update();
    }

    // Update is called once per frame
    int step = 0;
    void Update()
    {
        OnKeyEscape();

        OnKeyLeft();

        OnKeyRight();

        OnKeyUp();
        OnKeyDown();
        OnKeyReturn();

    }
    private void OnGUI()
    {
        if (XmlStage.curStage != null)
            GUI.Label(new Rect(10, 10, 1000, 40), "Page " + XmlStage.curStage.id.ToString());
    }
    public static void LoadLevel()
    {
        UIMgr.clearLayer();
        XmlStage.Clear();

    }

    public static void LoadPage(int nPage)
    {
        MediaList.Clear();
        UIMgr.clearLayer();

        XmlStage.LoadPage(nPage);
    }


    public static void LoadPrevPage(XmlPage pPage)
    {
        LoadPage(pPage.id - 1);
    }


    public static void LoadNextPage(XmlPage pPage)
    {
        LoadPage(pPage.id + 1);
    }




    void OnKeyLeft()
    {

    }


    void OnKeyRight()
    {

    }


    void OnKeyUp()
    {

    }

    void OnKeyDown()
    {

    }


    void OnKeyReturn()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0)
        || Input.GetKeyDown(KeyCode.Joystick3Button0)
        || Input.GetKeyDown(KeyCode.Joystick4Button0)
        || Input.GetKeyDown(KeyCode.Return)
|| Input.GetKeyDown((KeyCode)10) && UIMgr.uiItemList.Count != 0)
        {
            //if(audioSource.isPlaying)
            //{
            //    isYoyoInterduce = true;
            //}

            //   curEvent = pStage.btnList[pStage.nCurObj].clickEvent as XmlEvent;


        }
    }


    void OnKeyEscape()
    {
    if (!MediaList.Instance.objEditPanel.activeInHierarchy)
    {
      if (Input.GetKeyUp(KeyCode.Escape))
      {
        Application.Quit();
      }
      if (Input.GetMouseButtonDown(1))
      {
        Application.Quit();
      }
    }
  }

   

    //调用Android方法
    public static void CallAndroidMethod(string methodName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call(methodName);
        }
    }
    public static void CallAndroidMethod1(string methodName, string str)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass jc1 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo1 = jc1.GetStatic<AndroidJavaObject>("currentActivity");
            jo1.Call(methodName, str);
        }
    }

}
