using Action;
using Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XmlFile;

public class EventMgr : MonoBehaviour
{
  static EventMgr instance;

  // Use this for initialization
  void Start()
  {
    instance = this;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public static void startEvent(string evtName)
  {
    XmlEvent evt = XmlStage.GetEvent(evtName);
    startEvent(evt);
  }

  public static void startEvent(XmlEvent evt)
  {
    if (evt != null)
    {
      Event_Run event_Run = instance.gameObject.AddComponent<Event_Run>();

      event_Run.SetEvent(evt);
    }
  }

  public static void stopEvent()
  {
    // curEvent = null;
  }
}
