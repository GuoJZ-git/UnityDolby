using System.Xml;

namespace Action
{
    /// <summary>
    /// 音乐播放
    /// </summary>
    public class _play_music : ActBase
    {
        public string musicFile = "音乐文件";

        public _play_music()
        {
            actID = ACTION_ID.play_music;
            description = "播放音乐文件,file : resource中 音乐文件名，无扩展名";
        }

        public override void loadXML(XmlElement nodeAct)
        {
            base.loadXML(nodeAct);
            musicFile = nodeAct.GetAttribute("music");
            if (string.IsNullOrEmpty(musicFile))
                musicFile = nodeAct.GetAttribute("file");
        }

        public override void saveXml(XmlDocument xmlDoc, XmlElement nodeAct)
        {
            nodeAct.SetAttribute("file", musicFile);
        }

        public override void start()
        {
             AudioMgr.TurnOnMusic();
            MusicMgr.PlayDolby(musicFile);
            isEnd = true;
        }
    }
}