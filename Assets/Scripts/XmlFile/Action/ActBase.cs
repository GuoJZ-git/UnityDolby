using System;
using System.Xml;
using UnityEngine;

namespace Action
{
  public enum ACTION_ID
  {
    load_page,
    run_event,

    play_music,
    play_voice,
    play_sound,
    play_movie,
    show_subtitle,

    stop_music,
    stop_mov,


    add_pic,
    add_ani,
    add_toggle,
    add_button,
    disable_button,

    hide,
    set_pic,

    flash_pic,
    swing_pic,
    move_pic,
    fadeout_pic,

    set_event,

    wait,

    exit_app,
  };


  [Serializable]
  public abstract class ActBase
  {
    public string xmlText = "";

    public string description = "";

    public ACTION_ID actID;
    public Vector2 pos;

    public bool isEnd = false;

    public virtual void loadXML(XmlElement nodeAct)
    {
      xmlText = XmlFile.Formater.FormatXML(nodeAct.OuterXml);      // Debug.Log("Parse " + nodeAct.OuterXml);
    }

    public abstract void start();

    public abstract void saveXml(XmlDocument xmlDoc, XmlElement nodeAct);

    public static ActBase CreateAction(string strActID)
    {
      switch (strActID)
      {
        case "play_audio": return new _play_voice();
        case "play_voice": return new _play_voice();
        case "play_music": return new _play_music();
        case "play_sound": return new _play_sound();
        case "play_movie": return new _play_movie();
        case "stop_music": return new _stop_music();
        case "stop_movie": return new _stop_movie();
        case "run_event": return new _run_event();
        case "add_button": return new _show_button();
        case "hide": return new _hide();

        case "disable_button": return new _disable_button();
        default:
          Debug.LogError("Unknown Command " + strActID);
          break;
      }

      return null;
    }

  }
}