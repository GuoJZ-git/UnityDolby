using Action;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XmlFile;

namespace Pic2D
{
    public class _Button : _Show
    {
        _show_button actButton;

        public string strEvent;



        private EventTrigger eventTrigger;

        private void Awake()
        {
            eventTrigger = gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
                eventTrigger = gameObject.AddComponent<EventTrigger>();
        }
        private void RegisterEvent()
        {
            EventTrigger.Entry entry_down = new EventTrigger.Entry();
            entry_down.eventID = EventTriggerType.PointerDown;
            entry_down.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
            eventTrigger.triggers.Add(entry_down);

            //EventTrigger.Entry entry_up = new EventTrigger.Entry();
            //entry_up.eventID = EventTriggerType.PointerUp;
            //entry_up.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
            //eventTrigger.triggers.Add(entry_up);

            EventTrigger.Entry entry_click = new EventTrigger.Entry();
            entry_click.eventID = EventTriggerType.PointerClick;
            entry_click.callback.AddListener((data) => { OnButtonClick((PointerEventData)data); });
            eventTrigger.triggers.Add(entry_click);

        }
        // Use this for initialization
        void Start()
        {
            RegisterEvent();
            //if (button == null)
            //{
            //    button = gameObject.GetComponent<Button>();
            //    if (button != null)
            //        button.onClick.AddListener(OnButtonClick);
            //}
        }

        private void OnEnable()
        {
            //  InitButton();

        }

        public override void Init(ActBase act)
        {
            actButton = act as _show_button;
            strEvent = actButton.strEvent;

            XmlPic2D xml = XmlStage.GetAsset2D(actButton.assetID);
            if (xml != null)
            {
                xml.InitSprite();

                strEvent = actButton.strEvent;

                Sprite tex = xml.PicNormal;

                InitPos(tex, actButton.pos, xml.scale, 0);
            }
        }


        // Update is called once per frame
        void Update()
        {

        }


        public void OnPointerDown(BaseEventData arg0)//点选目录视频,eventTrigger事件
        {
            XmlPic2D toggle = XmlStage.GetAsset2D(actButton.assetID);

            if (toggle != null)
            {
                Image img = GetComponent<Image>();
                img.sprite = toggle.PicPress;
            }
        }
        public void OnPointerUp(BaseEventData arg0)//点选目录视频,eventTrigger事件
        {
            XmlPic2D toggle = XmlStage.GetAsset2D(actButton.assetID);

            if (toggle != null)
            {
                XmlEvent @event = XmlStage.GetEvent(strEvent);
                EventMgr.startEvent(@event);

                Image img = GetComponent<Image>();
                img.sprite = toggle.PicNormal;
            }
        }
        public void OnButtonClick(BaseEventData arg0)
        {
            XmlPic2D toggle = XmlStage.GetAsset2D(actButton.assetID);
            if (toggle != null)
            {
                Image img = GetComponent<Image>();
                img.sprite = toggle.PicNormal;
            }

            XmlEvent @event = XmlStage.GetEvent(strEvent);
            EventMgr.startEvent(@event);
        }
    }
}
