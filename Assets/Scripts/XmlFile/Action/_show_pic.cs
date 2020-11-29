using System.Xml;
using UnityEngine;

namespace Action
{
    public class _show_pic : ActBase
    {
        public int layer;
        public string itemID = "图片ID";
        public string assetID = "资源ID";
        public float angle = 0; //"显示旋转角度";


        public _show_pic()
        {
            actID = ACTION_ID.add_pic;
            description = "显示图片……载入资源，并显示。asset:为资源名，来源Xml文件Assets节点中设定的名\n" +
                "\t\tid: 资源载入后，在场景中的实例ID";
        }


        public override void loadXML(XmlElement nodeAct)
        {
            base.loadXML(nodeAct);

            itemID = nodeAct.GetAttribute("id");
            assetID = nodeAct.GetAttribute("name");
            if (string.IsNullOrEmpty(assetID))
                assetID = nodeAct.GetAttribute("asset");
            if (string.IsNullOrEmpty(assetID))
                Debug.LogError("显示图片错误, name不能为空\n" + nodeAct.OuterXml);

            if (string.IsNullOrEmpty(itemID))
                itemID = assetID;

            layer = nodeAct.getInt("layer");

            pos.x = nodeAct.getFloat("x");
            pos.y = nodeAct.getFloat("y");

            angle = nodeAct.getFloat("angle");
        }

        public override void saveXml(XmlDocument xmlDoc, XmlElement nodeAct)
        {

            nodeAct.SetAttribute("id", itemID);
            nodeAct.SetAttribute("asset", assetID);
            nodeAct.SetAttribute("layer", layer.ToString());
            nodeAct.SetAttribute("x", pos.x.ToString());
            nodeAct.SetAttribute("y", pos.y.ToString());
            nodeAct.SetAttribute("angle", angle.ToString());
        }

        public override void start()
        {
            //Debug.Log("Run Action : " + actID + " " + this.name);

            GameObject objLayer = UIMgr.CreateLayer(layer);
            if (objLayer == null)
                return;
            objLayer.SetActive(true);

            if (string.IsNullOrEmpty(assetID))
                return;

            Object2D info = UIMgr.GetItemInfo(itemID);
            GameObject objPic = null;
            if (info != null)
                objPic = info.gameObject;
            else
            {
                objPic = UIMgr.CreatePic(objLayer);
                objPic.name = itemID;
             }

            info.AddAction(this);

            this.isEnd = true;
        }
    }
}