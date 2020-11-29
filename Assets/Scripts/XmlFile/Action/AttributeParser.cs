using System.Xml;
using UnityEngine;

namespace Action
{
    public static class AttributeParser
    {
        public static bool getBool(this XmlElement node, string att)
        {
            string str = node.GetAttribute(att);
            if (!string.IsNullOrEmpty(str))
                return bool.Parse(str);
            else
                return false;
        }

        public static string getString(this XmlElement node, string att)
        {
            return node.GetAttribute(att);
        }

        public static int getInt(this XmlElement node, string att, int default_value = 0)
        {
            string strLayer = node.GetAttribute(att);
            if (string.IsNullOrEmpty(strLayer) == false)
                return int.Parse(strLayer);

            return default_value;
        }
        public static float getFloat(this XmlElement node, string att)
        {
            string strLayer = node.GetAttribute(att);
            if (string.IsNullOrEmpty(strLayer) == false)
                return float.Parse(strLayer);

            return 0;
        }

        public static Vector2 getVector2(this XmlElement node, string str)
        {
            string strScale = node.GetAttribute(str);
            if (!string.IsNullOrEmpty(strScale))
            {
                strScale = strScale.Replace("(", "").Replace(")", "");
                string[] s = strScale.Split(',');
                return new Vector2(float.Parse(s[0]), float.Parse(s[1]));
            }
            else
            {
                return Vector2.one;
            }
        }

        public static Vector2 getVector3(this XmlElement node, string str)
        {
            string strScale = node.GetAttribute(str);
            if (!string.IsNullOrEmpty(strScale))
            {
                strScale = strScale.Replace("(", "").Replace(")", "");
                string[] s = strScale.Split(',');
                return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
            }
            else
            {
                return Vector3.one;
            }
        }
    }
}