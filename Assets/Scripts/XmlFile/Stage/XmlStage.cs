using System.Collections.Generic;

namespace XmlFile
{

    public class XmlStage
    {
    static Dictionary<int, XmlPage> stageList = new Dictionary<int, XmlPage>();
    public static XmlPage curStage =null;


        public static XmlPic2D GetAsset2D(string assetName)
        {
          return  curStage.GetXmlPic2D(assetName);
        }


        public static void Clear()
        {
            XmlStage.stageList.Clear();

        }
        public static void LoadPage(int nPage)
        {
            if (nPage < 0) return;
            if (stageList.ContainsKey(nPage) == false)
            {
                XmlPage newStage = new XmlPage(nPage);
                newStage.LoadXML();
                stageList[nPage] = newStage;// XmlPage.loadXML(nPage);
            }
            curStage = stageList[nPage];
            curStage.RunInitEvent();
        }
        public static XmlEvent GetEvent(string strEvent)
        {
            if (curStage!=null)
            return curStage.GetEvent(strEvent);
            return null;

        }
    }
}
