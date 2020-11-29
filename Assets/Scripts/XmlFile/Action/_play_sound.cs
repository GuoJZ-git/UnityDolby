using System.Xml;

namespace Action
{
    public class _play_sound : ActBase
    {
        public string soundFile = "音乐文件";

        public _play_sound()
        {
            actID = ACTION_ID.play_sound;
            description = "播放音效文件,file : resource中 音效文件名，无扩展名";
        }

        public override void loadXML(XmlElement nodeAct)
        {
            base.loadXML(nodeAct);
                 soundFile = nodeAct.GetAttribute("file");
        }

        public override void saveXml(XmlDocument xmlDoc, XmlElement nodeAct)
        {
            nodeAct.SetAttribute("file", soundFile);
        }

        public override void start()
        {
      SoundMgr.PlaySound(soundFile);
            isEnd = true;
        }
    }
}