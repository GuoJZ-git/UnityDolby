using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XmlFile
{
  public class Formater
  {
    public static string FormatXML(string str)
    {
      System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
      doc.LoadXml(str);
      System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
      System.IO.StringWriter stringWriter = new System.IO.StringWriter(stringBuilder);
      System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(stringWriter);
      xmlWriter.Formatting = System.Xml.Formatting.Indented;
      doc.WriteTo(xmlWriter);
      return stringBuilder.ToString();
    }
  }
}