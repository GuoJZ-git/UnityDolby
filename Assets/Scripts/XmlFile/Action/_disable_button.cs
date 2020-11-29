using System.Xml;
using UnityEngine.EventSystems;

namespace Action
{
    /// <summary>
    /// 失效按钮
    /// </summary>
    public class _disable_button : ActBase
    {
        string itemID;

        public _disable_button()
        {
            actID = ACTION_ID.disable_button;
            description = "按钮失效";
        }

        public override void loadXML(XmlElement nodeAct)
        {
            base.loadXML(nodeAct);
            itemID = nodeAct.GetAttribute("_id");
        }

        public override void saveXml(XmlDocument xmlDoc, XmlElement nodeAct)
        {
            nodeAct.SetAttribute("_id", itemID);
        }

        public override void start()
        {
 
            Object2D info = UIMgr.GetItemInfo(itemID);

            EventTrigger trigger = info.gameObject.GetComponent<EventTrigger>();
            trigger.enabled = false;
        }

      
    }
}