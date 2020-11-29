using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
 
 
    namespace Action
    {
        public class _run_event : ActBase
        {

            public string eventID;
            public int mode;

            public _run_event()
            {
                actID = ACTION_ID.run_event;
                description = "执行";
            }

            public override void loadXML(XmlElement nodeAct)
            {
                base.loadXML(nodeAct);

                eventID = nodeAct.GetAttribute("event");
                mode = nodeAct.getInt("mode");
            }


            public override void saveXml(XmlDocument xmlDoc, XmlElement nodeAct)
            {
                nodeAct.SetAttribute("event", eventID);
                nodeAct.SetAttribute("mode", mode.ToString());
            }

            public override void start()
            {
                EventMgr.startEvent(eventID);
                this.isEnd = true;
            }

            // Update is called once per frame
            void Update()
            {

            }
        }
    }
