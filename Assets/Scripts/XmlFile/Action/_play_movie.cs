using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Action
{

    public class _play_movie : ActBase
    {
        public string movFile;

        public _play_movie()
        {
            this.actID = ACTION_ID.play_movie;
            description = "播放视频";
        }

        public override void loadXML(XmlElement nodeAct)
        {
            base.loadXML(nodeAct);
            movFile = nodeAct.GetAttribute("file");
        }

        public override void saveXml(XmlDocument xmlDoc, XmlElement nodeAct)
        {
            nodeAct.SetAttribute("file", movFile);
        }

    public override void start()
    {
      GameObject objLayer = UIMgr.CreateLayer(10);
      if (objLayer != null)
      {
        objLayer.SetActive(true);

        VideoFile theVideo = VideoMgr.CreateVideoPanel(movFile, objLayer);

        UnityEngine.Video.VideoClip clip = AssetLoader.Load<UnityEngine.Video.VideoClip>(movFile);
        theVideo.Play(clip);
      }

      this.isEnd = true;
        }
 
    }
}