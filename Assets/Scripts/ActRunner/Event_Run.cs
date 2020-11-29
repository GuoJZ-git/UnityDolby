using Action;
 using UnityEngine;
using XmlFile;

namespace Runner
{
  public class Event_Run : MonoBehaviour
  {

    _run_event actRun;

    public XmlEvent theEvent = null;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
      if (theEvent != null)
      {
        theEvent.update();
        if (theEvent.curAction == theEvent.actList.Count)
        {
          if (actRun != null && actRun.mode == 1)
          {
            theEvent.Start();
          }
          else
          {
            Destroy(this);
          }
        }
      }
    }

    public void SetEvent(XmlEvent evt)
    {
      actRun = null;

      theEvent = evt;// AppMgr.getEvent(actRun.eventID);


      if (theEvent != null)
        theEvent.Start();
      else
        Debug.Log("Event NULL");
    }

    public void SetEvent(_run_event act)
    {
      actRun = act;

      theEvent = XmlStage.GetEvent(actRun.eventID);


      if (theEvent != null)
        theEvent.Start();
      else
        Debug.Log("Event NULL");
    }
  }
}