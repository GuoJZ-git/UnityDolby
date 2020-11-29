using System.Xml;
using UnityEngine;
using XmlFile;

namespace Action
{
    /// <summary>
    /// 显示按钮
    /// </summary>
    public class _show_button : ActBase
    {
        public string itemID = "按钮ID";
        public int layerNo = 0;
        public string assetID = "资源ID";
        public string strEvent = "点击事件";


        public _show_button()
        {
            actID = ACTION_ID.add_button;
            description = "显示按钮……载入资源，并显示\n" +
                "\t\tid: 资源载入后，在场景中的实例ID";
        }

        public override void loadXML(XmlElement nodeAct)
        {
            base.loadXML(nodeAct);
            itemID = nodeAct.GetAttribute("id");
            assetID = nodeAct.GetAttribute("name");
            if (string.IsNullOrEmpty(assetID))
                assetID = nodeAct.GetAttribute("asset");

            if (string.IsNullOrEmpty(itemID))
                itemID = assetID;

            layerNo = nodeAct.getInt("layer");
            pos.x = nodeAct.getFloat("x");
            pos.y = nodeAct.getFloat("y");

            strEvent = nodeAct.getString("event");
            if (string.IsNullOrEmpty(strEvent))
                strEvent = itemID;


        }

        public override void saveXml(XmlDocument xmlDoc, XmlElement nodeAct)
        {
            nodeAct.SetAttribute("id", itemID);
            nodeAct.SetAttribute("asset", assetID);
            nodeAct.SetAttribute("layer", layerNo.ToString());
            nodeAct.SetAttribute("x", pos.x.ToString());
            nodeAct.SetAttribute("y", pos.y.ToString());
            nodeAct.SetAttribute("event", strEvent);

        }

        public override void start()
        {
 
            GameObject objLayer = UIMgr.CreateLayer(layerNo);
            if (objLayer == null)
                return;

      objLayer.transform.parent.gameObject.SetActive(true);
      objLayer.SetActive(true);

            Object2D info = UIMgr.GetItemInfo(itemID);
            GameObject objPic = null;
            if (info != null)
                objPic = info.gameObject;
            else
            {
                objPic = UIMgr.CreatePic(objLayer);
                objPic.name = itemID;
            }

            Object2D picShow = objPic.GetComponent<Object2D>();
            picShow.AddAction(this);

            this.isEnd = true;

         }
    }
}