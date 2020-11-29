using UnityEngine;
using System;
using System.Collections;
using System.Xml;
using Action;
using System.Collections.Generic;
using System.Text;

namespace XmlFile
{
    /// <summary>
    /// named action group 
    /// </summary>
    [Serializable]
    public class XmlEvent
    {
       public string xmlText = "";

        public string eventID;
        [SerializeField]
        public List<ActBase> actList = new List<ActBase>();

        public int curAction = -1;


        public void Start()
        {
            curAction = 0;
            ActBase act = CurAction();
            
     
            while (act != null)
            {
                if (runAction(act))
                    act = NextAction();
                else
                    break;
            }  
        }

        public void SaveXml(XmlDocument xmlDoc, XmlNode nodeEvent)
        {
             foreach (ActBase act in actList)
            {
                //  XmlElement nodeAct = xmlDoc.CreateElement("action");
                XmlElement nodeAct = xmlDoc.CreateElement(act.actID.ToString());// "action");
                act.saveXml(xmlDoc, nodeAct);
                nodeEvent.AppendChild(nodeAct);
            }
 
        }
      
      

        public static XmlEvent LoadXml(XmlNode nodeEvent)
        {
            if (nodeEvent == null) return null;
                XmlEvent newEvent = new XmlEvent();

              newEvent.xmlText = Formater.FormatXML(nodeEvent.OuterXml);
            ////////////////////////////////////////////////////////////////////////

            XmlNodeList actList = nodeEvent.SelectNodes("action");
            newEvent.eventID = ((XmlElement)nodeEvent).getString("id");
            if (actList.Count > 0)
            {
                foreach (XmlElement nodeAct in actList)
                {
                    string strAct = nodeAct.GetAttribute("cmdID");
                    if (string.IsNullOrEmpty(strAct))
                        strAct = nodeAct.GetAttribute("cmd");

                    ActBase newAct = ActBase.CreateAction(strAct);
                    if (newAct != null)
                    {
                        newAct.loadXML(nodeAct);
                        newEvent.actList.Add(newAct);
                    }
                    else
                    {
                        Debug.Log(nodeAct.OuterXml);
                    }
                }
                return newEvent;
            }

            actList = nodeEvent.ChildNodes;
            if (actList.Count > 0)
            {
                foreach (XmlElement nodeAct in actList)
                {
                    ActBase newAct = ActBase.CreateAction(nodeAct.Name);
                    if (newAct != null)
                    {
                        newAct.loadXML(nodeAct);
                        newEvent.actList.Add(newAct);
                    }
                    else
                    {
                        Debug.Log(nodeAct.OuterXml);
                    }
                }
                return newEvent;
            }


            return newEvent;
        }

        public ActBase NextAction()
        {
            curAction++;
            if (curAction < 0)
                return null;
            if (curAction >= actList.Count)
                return null;
            return actList[curAction] as ActBase;
         }

        public ActBase CurAction()
        {
            if (curAction < 0)
                return null;
            if (curAction >= actList.Count)
                return null;
            return actList[curAction] as ActBase;
        }


        public void update()
        {
            ActBase act = CurAction();
            if (act == null)
                return;

            while (act.isEnd)
            {
                act = NextAction();
                if (act == null)
                    break;
                runAction(act);
            }
        }

        public bool runAction(ActBase act)
        {
            if (act != null)
            {
                act.start();
                return act.isEnd;
            }
            return true;
        }
    }
}