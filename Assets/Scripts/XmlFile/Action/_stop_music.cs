using System.Xml;

namespace Action
{
    public class _stop_music : ActBase
    {
        public _stop_music()
        {
            actID = ACTION_ID.stop_music;
            description = "停止音乐";
        }

        public override void start()
        {
             MusicMgr.StopMusic();

            isEnd = true;
        }

        public override void loadXML(XmlElement nodeAct)
        {
            base.loadXML(nodeAct);
        }

        public override void saveXml(XmlDocument xmlDoc, XmlElement nodeAct)
        {
            //throw new System.NotImplementedException();
        }
    }
}