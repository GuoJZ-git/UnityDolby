using Action;
using UnityEngine;
using XmlFile;

public class Object2D : MonoBehaviour
{
    public ActBase actAdd; //add_pic, add_button, add_toggle

    T GetExec<T>() where T : MonoBehaviour
    {
        T t = null;
        if (t == null)
        {
            t = gameObject.GetComponent<T>();
        }

        if (t == null)
        {
            t = gameObject.AddComponent<T>();
        }

        return t;
    }


    // Use this for initialization
    void Start()
    {

    }

     

    // Update is called once per frame
    void Update()
    {

    }

 
 
    public string ItemName
    {
        get
        {
            return gameObject.name;
        }
    }

  public void AddAction(ActBase act)
  {
    switch (act.actID)
    {
      
      case ACTION_ID.add_button:
        Pic2D._Button button2D = GetExec<Pic2D._Button>();
        button2D.Init(act);
        button2D.enabled = true;
        break; 
    }
  }

}
