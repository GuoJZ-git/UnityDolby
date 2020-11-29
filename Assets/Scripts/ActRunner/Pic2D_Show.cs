using Action;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XmlFile;



namespace Pic2D
{
    public class _Show : MonoBehaviour
    {
        _show_pic actPic;



        // Use this for initialization
        void Start()
        {

        }
        public virtual void Init(ActBase a)
        {
            actPic = a as _show_pic;
            XmlPic2D xmlPic = XmlStage.GetAsset2D(actPic.assetID);
            if (xmlPic == null)
            {
                Debug.LogError("显示图片错误，找不到文件按  " + actPic.assetID + "in stage " + XmlStage.curStage.id);
                return;
            }
            xmlPic.InitSprite();
            Sprite tex = xmlPic.PicNormal;// Resources.Load<Sprite>(XmlStage.filePath +  btn.picNormal.fileName);

            InitPos(tex, actPic.pos, xmlPic.scale, actPic.angle);
        }

        public void InitPos(Sprite tex, Vector2 pos, Vector2 scale, float angle)
        {

            if (tex == null)
                return;

            Rect rect = new Rect();
            rect.x = pos.x;
            rect.y = 768 - pos.y - tex.texture.height * scale.y;
            rect.width = tex.texture.width;
            rect.height = tex.texture.height;

            Image img = GetComponent<Image>();
            if (img != null)
            {
                img.sprite = tex;
                img.SetNativeSize();
            }

            RectTransform rt = GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.sizeDelta = rect.size;

                //Vector2 pos = rect.position;
                //  pos.y -= tex.texture.height;
                pos.y = 768 - pos.y - tex.texture.height * scale.y;
                rt.position = pos;

                Vector3 center = new Vector3(rect.center.x, rect.center.y, 0);
                //rt.pivot = new Vector2(0.5f, 0.5f);
                rt.transform.RotateAround(center, Vector3.forward, angle);
                rt.transform.localScale = new Vector3(scale.x, scale.y, 1);
                rt.pivot = new Vector2(0, 0);
            }

            gameObject.SetActive(true);
        }

        public void SetPic(string fileName)
        {
            Sprite tex = AssetLoader.Load<Sprite>(fileName);
            Image img = GetComponent<Image>();
            if (img != null)
            {
                img.sprite = tex;
                img.SetNativeSize();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}