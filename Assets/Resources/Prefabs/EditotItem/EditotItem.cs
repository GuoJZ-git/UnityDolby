using UnityEngine;
using UnityEngine.UI;
using Action;
using XmlFile;
using System.Text;

public class EditotItem : MonoBehaviour
{

  public Text label;

  Object2D info;
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (info != null)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(info.ItemName);

      if (info.actAdd != null)
      {
        sb.AppendFormat(" ({0:F0}，{1:F0}）", info.actAdd.pos.x, info.actAdd.pos.y);
        sb.Append(info.actAdd.actID);
      }
      label.text = sb.ToString();
    }
  }

  private void OnDestroy()
  {
    info = null;
  }

  public void InitItem(GameObject ob, ActBase act)
  {
    info = ob.GetComponent<Object2D>();


    label.text = string.Format("{3},{0} ({1:F0}，{2:F0}）", ob.name, act.pos.x, act.pos.y, act.actID);

    UnityEngine.UI.Toggle toggle = GetComponent<UnityEngine.UI.Toggle>();

    toggle.onValueChanged.AddListener(
        delegate (bool check)
        {

          if (check)
          {
             label.color = Color.red;
          }
          else
          {
            label.color = Color.black;
          }
        }
    );
  }

}
