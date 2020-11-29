using System.Xml;
using UnityEngine;

namespace Action
{
    /// <summary>
    /// 语音播放
    /// </summary>
    public class _play_voice : ActBase
    {
        public string audioFile = "语音文件";

        public _play_voice()
        {
            this.actID = ACTION_ID.play_voice;
            description = "播放语音文件,file : resource中 语音文件名，无扩展名";
        }

        public override void loadXML(XmlElement nodeAct)
        {
            base.loadXML(nodeAct);

            audioFile = nodeAct.GetAttribute("sound");
            if (string.IsNullOrEmpty(audioFile))
                audioFile = nodeAct.GetAttribute("file");

        }

        public override void saveXml(XmlDocument xmlDoc, XmlElement nodeAct)
        {
            nodeAct.SetAttribute("file", audioFile);
        }
        public override void start()
        {
             VoiceMgr.PlayVoice(audioFile, Vector3.zero);
            isEnd = true;
        }
    }
}