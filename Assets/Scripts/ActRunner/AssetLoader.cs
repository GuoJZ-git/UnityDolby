using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XmlFile;

public static class AssetLoader  {

    public static string assetPath;
    public static string xmlPath;

   
    
    static AssetLoader()
    {
        assetPath = Application.productName + "/Assets/";
        xmlPath = Application.productName + "/Xml/";

     }

    public static T Load<T>( string strFile ) where T : Object
    {
         // Debug.Log("载入资源" + strFile);
        T s = Resources.Load<T>(assetPath + strFile);
        if (s == null)
        {
            string strLog = string.Format("{0}, 找不到文件, {1}, stage {2} ", typeof(T), assetPath + strFile, "unknown stage");// AppMgr.curStage.id);
            Debug.LogError(strLog);
        }

        return s;
    }
}
