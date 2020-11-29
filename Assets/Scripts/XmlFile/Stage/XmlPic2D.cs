using Action;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine;

namespace XmlFile
{
    /// <summary>
    /// 序列帧资源设定
    /// </summary>
    public class XmlPic2D 
    {
        public string itemID = "";
        public string[] strFile;        //序列帧文件名列表
        public Vector2 scale;

        #region Ani
       // public Sprite[] picAni;
        #endregion

        public void InitSprite()
        {
            int frameCount = 0;
          //  picAni = new Sprite[strFile.Length];
            for (int i = 0; i < strFile.Length; i++)
            {
                //string strName = string.Format("{0}{1:00}", a.fileName, i);
                Sprite t = AssetLoader.Load<Sprite>(strFile[i]);
               // if (t != null)
                {
                //    picAni[i] = t;
                    frameCount++;
                }
            }
           
        }

        public Sprite PicNormal
        {
            get
            {
                if (strFile[0]!=null)
                    return AssetLoader.Load<Sprite>(strFile[0]);

                return null;
            }
        }

        public Sprite PicPress
        {
            get
            {
                if (strFile[1] != null)
                    return AssetLoader.Load<Sprite>(strFile[1]);
                return null;
            }
        }

        static int stringStartWith(string str)
        {
            Regex regChina = new Regex("^[^\x00-\xFF]");
            Regex regNum = new Regex("^[0-9]");
            Regex regChar = new Regex("^[a-z]");
            Regex regDChar = new Regex("^[A-Z]");
            //  string str = "YL闫磊"; 
            if (regNum.IsMatch(str))
            {
                return 1;//  MessageBox.Show("是数字");
            }
            else if (regChina.IsMatch(str))
            {
                return 2;//MessageBox.Show("是中文");
            }
            else if (regChar.IsMatch(str))
            {
                return 3;//MessageBox.Show("小写");
            }
            else if (regDChar.IsMatch(str))
            {
                return 4;//MessageBox.Show("大写");
            }
            return 0;
        }

            public static XmlPic2D LoadXml(XmlElement element)
        {

                    XmlPic2D frame = new XmlPic2D();
            switch (element.Name.ToLower())
            {
                case "frame2d":
                case "pic":
                case "pic2d":
                case "toggle":
                case "button":
                case "item":
                    string itemID = element.GetAttribute("name");
                    if (string.IsNullOrEmpty(itemID))
                        itemID = element.GetAttribute("id");
                    if (stringStartWith(itemID) == 1)
                        frame.itemID = "Pic" + itemID;
                    else
                        frame.itemID = itemID;
                    frame.parseXml(element);
                     break;
                default:
                    if(stringStartWith(element.Name)==1)
                    frame.itemID = "Pic"+element.Name;
                    else
                    frame.itemID = element.Name;
                    frame.parseXml(element);
                    break;

            }

            return frame;
        }


        internal   void parseXml(XmlElement nodeButton)
        {

            scale = nodeButton.getVector2("scale");

            XmlNodeList nodeFramList = nodeButton.SelectNodes("frame");
            strFile = new string[nodeFramList.Count];
            for (int i = 0; i < nodeFramList.Count; i++)
            {
                XmlElement node = nodeFramList[i] as XmlElement;
                strFile[i] = node.GetAttribute("file");
            }

        //    picAni = new Sprite[strFile.Length];
        }

        public   XmlElement saveXml(XmlDocument xmlDoc)
        {
           
            XmlElement nodeButton = xmlDoc.CreateElement("Pic2D");
            nodeButton.SetAttribute("id", itemID);// 

            nodeButton.SetAttribute("scale", scale.ToString("F2"));

            for (int i = 0; i < strFile.Length; i++)
            {
                XmlElement node = xmlDoc.CreateElement("frame");
                node.SetAttribute("file", strFile[i]);
                nodeButton.AppendChild(node);
            }
            return nodeButton;
        }
    }

}