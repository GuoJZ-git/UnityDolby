using System.Xml;

namespace Action
{
    public class _stop_movie : ActBase
    {
        public _stop_movie()
        {
            actID = ACTION_ID.stop_mov;
            description = "停止视频";
        }

        public override void start()
        {
             VideoMgr.Stop();

            isEnd = true;
        }


        public override void loadXML(XmlElement nodeAct)
        {
            base.loadXML(nodeAct);
        }

        public override void saveXml(XmlDocument xmlDoc, XmlElement nodeAct)
        {
           // throw new System.NotImplementedException();
        }
    }
}