using System.Xml;
using UnityEngine;

namespace Action
{
  public class _hide : ActBase
  {
    private int layerNo;
    public string targetID;

    public _hide()
    {
      actID = ACTION_ID.hide;
      description = "隐藏图片";
    }

    public override void loadXML(XmlElement nodeAct)
    {
      base.loadXML(nodeAct);

      layerNo = nodeAct.getInt("layer", -1);
      targetID = nodeAct.getString("name");
      if (string.IsNullOrEmpty(targetID))
        targetID = nodeAct.getString("_id");

    }

    public override void saveXml(XmlDocument xmlDoc, XmlElement nodeAct)
    {
      nodeAct.SetAttribute("layer", layerNo.ToString());
      nodeAct.SetAttribute("_id", targetID);
    }

    public override void start()
    {
      UIMgr.HideLayer(layerNo);
      UIMgr.HideItem(targetID);
      UIMgr.HideAll();

      this.isEnd = true;
    }


  }
}