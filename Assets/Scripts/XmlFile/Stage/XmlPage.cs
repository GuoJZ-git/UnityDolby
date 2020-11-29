using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
using System.Xml;
using System.Collections.Generic;
using Action;
using System.Data;

namespace XmlFile
{

    /// <summary>
    /// 图片展示页设定类
    /// </summary>
    public class XmlPage
    {
        readonly string XmlFileName;

        public int id;          //页序号
        public XmlEvent eventInit = new XmlEvent();    //初始化设定

        public IList<XmlPic2D> assetList = new List<XmlPic2D>();
        public Dictionary<string, XmlEvent> eventList = new Dictionary<string, XmlEvent>();

        public XmlPage(int n)
        {
 
               id = n;

            XmlFileName = string.Format("{0}/xml/stage_{1:00}", Application.productName, id);
        }

        public   XmlPage LoadXML( )
        {
          //  eventList.Clear();

              //string fileName = levelFile.Split('.')[0];
            TextAsset fileContent = Resources.Load<TextAsset>(XmlFileName);
            if (fileContent == null)
            {
                return null;
            }
            string data = fileContent.text.ToString();

             ParseXML(data);

            Resources.UnloadAsset(fileContent);

            return this;
        }

        public void ParseXML(string smlString)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(smlString);
            XmlNode nodeRoot = xmlDoc.SelectSingleNode("stage");

            LoadXML(nodeRoot);
        }


        public void LoadXML( XmlNode xmlRoot)
        {
            XmlNode nodeInit = xmlRoot.SelectSingleNode("Init");
            if (nodeInit == null)
            {
                nodeInit = xmlRoot.SelectSingleNode("begin");
             }

            if (nodeInit != null)
            {
                eventInit = XmlEvent.LoadXml(nodeInit);
                eventInit.eventID = "Init";
            }
            XmlNode nodeItem = xmlRoot.SelectSingleNode("Assets");
            if (nodeItem == null)
                nodeItem = xmlRoot.SelectSingleNode("Items");
            if (nodeItem != null)
            {
                for (int i = 0; i < nodeItem.ChildNodes.Count; i++)
                {
                    XmlNode node = nodeItem.ChildNodes[i];// 
                    if (node.NodeType != XmlNodeType.Element)
                        continue;

                    XmlElement element = node as XmlElement;
                    XmlPic2D item = XmlPic2D.LoadXml(element);
                    if (item !=null)
                        assetList.Add(item); 
                }
            }

            XmlNode nodeEvent = xmlRoot.SelectSingleNode("Events");
            if (nodeEvent != null)
            {
                XmlNodeList eList = nodeEvent.SelectNodes("OnClick");
                foreach (XmlElement node in eList)
                {
                    XmlEvent newEvent = XmlEvent.LoadXml(node);
                    if (newEvent!=null)
                    eventList[newEvent.eventID] = newEvent;
                }
            }
        }

        public bool SaveXML( )
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement xmlRoot = xmlDoc.CreateElement("stage");
            xmlRoot.SetAttribute("Page", id.ToString());
            xmlRoot.SetAttribute("Count", assetList.Count.ToString());

            XmlNode begin = xmlDoc.CreateElement("Init");
            foreach (ActBase act in eventInit.actList)
            {
                XmlElement nodeAct = xmlDoc.CreateElement(act.actID.ToString());// "action");
                act.saveXml(xmlDoc, nodeAct);
                begin.AppendChild(nodeAct);
            }          
            xmlRoot.AppendChild(begin);

            ///////////////////////////////////////////////////////

            XmlNode items = xmlDoc.CreateElement("Assets");
            foreach (XmlPic2D item in assetList)
            {
                XmlElement nodeItem = item.saveXml(xmlDoc);
                items.AppendChild(nodeItem);

              
            }
            xmlRoot.AppendChild(items);

            ///////////////////////////////////////////////////////
            XmlNode events = xmlDoc.CreateElement("Events");
            foreach ( XmlEvent xml in   eventList.Values)
            {
                XmlElement nodeEvent = xmlDoc.CreateElement("OnClick");
                nodeEvent.SetAttribute("id", xml.eventID);
                xml.SaveXml(xmlDoc, nodeEvent);
                events.AppendChild(nodeEvent);
            }
            xmlRoot.AppendChild(events);
            
            ///////////////////////////////////////////////////////
            //XmlNode Example = xmlDoc.CreateElement("样例");

            //foreach (var temp in Enum.GetNames(typeof(ACTION_ID)))
            //{
            //    ActBase act = ActBase.CreateAction(temp.ToString());

            //    XmlComment node = xmlDoc.CreateComment("说明");
            //    node.InnerText = act.description;
            //    Example.AppendChild(node);

            //    XmlElement nodeAct = xmlDoc.CreateElement(act.actID.ToString());// "action");
            //    act.saveXml(xmlDoc, nodeAct);

            //    Example.AppendChild(nodeAct);
            //}
            //xmlRoot.AppendChild(Example);
            ///////////////////////////////////////////////////////

            xmlDoc.AppendChild(xmlRoot);

            string levelFile = string.Format(Application.dataPath+"/Resources/"+AssetLoader.xmlPath+"stage_{0:00}.xml", id);
            
            xmlDoc.Save(levelFile);
            Debug.Log("XML saved!\t"+ levelFile);
            return true;
        }

        public void RunInitEvent()
        {
            EventMgr.startEvent(eventInit);
        }


        // Update is called once per frame
        public void Update()
        {
         
        }
         

        public   XmlEvent GetEvent(string strEvent)
        {
            if (string.IsNullOrEmpty(strEvent))
                return null;

            if ( eventList.ContainsKey(strEvent))
                return  eventList[strEvent];

            return null;
        }
     
        public XmlPic2D GetXmlPic2D(string itemName) 
        {
            if (string.IsNullOrEmpty(itemName))
                return null;

            foreach (var item in assetList)
            {
                if (item.itemID == itemName)
                {
                        return item  ;
                }
            }

            Debug.LogError(  "XmlPic2D 找不到 -- " + itemName);
            return null;
        }

    }  
}
