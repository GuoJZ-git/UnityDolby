using UnityEngine;
using UnityEngine.UI;
using XmlFile;

public class EditorPanel : MonoBehaviour
{

  public GameObject item;
  public GameObject parent;

  static EditorPanel instance;

  private void Awake()
  {
    Instance = this;

  }
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
  }


  private void OnEnable()
  {
    Time.timeScale = 0;
    InitPanel();
  }

  private void OnDisable()
  {
    Clear();
    Time.timeScale = 1;
  }

  public void InitPanel()
  {
    foreach (Transform child in parent.transform)
    {
      Destroy(child.gameObject);
    }

    //foreach (Object2D info in UIMgr.uiItemList.Values)
    //{
    //  AddItem(info);
    //}
  }



  private void LateUpdate()
  {
    parent.transform.SetAsLastSibling();
  }

  public static void Clear()
  {
    if (Instance == null)
      return;

    for (int i = 0; i < Instance.parent.transform.childCount; i++)
    {
      Transform transform = Instance.parent.transform.GetChild(i);
      GameObject.Destroy(transform.gameObject);
    }
  }

  public ToggleGroup group;

  public static EditorPanel Instance { get => instance; set => instance = value; }

  public static Button AddItem(string mediaName)
  {
    if (Instance != null)
      return Instance.addItem(mediaName);

    return null;
  }

  public Button addItem(string mediaName)
  {
    GameObject a = Instantiate(item, parent.transform) as GameObject;
    a.name = mediaName;

   // EditotItem uo = a.GetComponent<EditotItem>();
   // uo.InitItem(info.gameObject, info.actAdd);

    UnityEngine.UI.Button button = a.GetComponent<UnityEngine.UI.Button>();
    button.GetComponentInChildren<Text>().text = mediaName;

    // toggle.isOn = false;
    //toggle.group = group;
    return button;
  }
  public static void RemoveItem(string objName)
  {
    if (Instance != null)
      Instance.removeItem(objName);
  }

  public void removeItem(string objName)
  {
    Transform child = parent.transform.Find(objName);
    if (child != null)
      Destroy(child.gameObject);



  }

  private void OnGUI()
  {

  } 

}
