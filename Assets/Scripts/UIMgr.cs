using Action;
using System.Collections.Generic;
using UnityEngine;
using XmlFile;

public class UIMgr : MonoBehaviour
{
    public static UIMgr instance = null;

    public GameObject canvas;

    GameObject prefabLayer;
    GameObject prefabBtn;
    GameObject prefabPic;
    
    public static Vector2 Resolution = new Vector2(1920, 1080);

    public static int MAX_LAYER = 32;
    static SortedList<int, GameObject> lstLayer = new SortedList<int, GameObject>();

    [SerializeField]
    public static Dictionary<string, Object2D> uiItemList = new Dictionary<string, Object2D>();

    public static void clearLayer()
    {
        foreach (var lay in lstLayer )// nt i = 0; i < MAX_LAYER; i++)
        {
            Destroy(lay.Value);
         //   lstLayer[lay.Key] = null;
        }
        lstLayer.Clear();

        uiItemList.Clear();

        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 删除实例化的场景物件记录
    /// </summary>
    /// <param name="itemName"></param>
    public static void RemoveFromList(string itemName)
    {
        if(uiItemList.ContainsKey(itemName))
        uiItemList.Remove(itemName);

        EditorPanel.RemoveItem(itemName);
    }

    

    bool Main()
    {
        Screen.fullScreen = true;
        Screen.SetResolution(1920, 1080, true);

        return true;
    }

    void Awake()
    {
        //  Handheld.PlayFullScreenMovie("start.mov", Color.black, FullScreenMovieControlMode.Hidden);
        Screen.fullScreen = true;
        Screen.SetResolution(1024, 768, true);
    }

    // Use this for initialization
    void Start()
    {

        instance = this;



        //  StartCoroutine(CoroutinePlayMovie());
        prefabLayer = Resources.Load<GameObject>("Prefabs/layer");
         prefabBtn = Resources.Load<GameObject>("Prefabs/Button/btn");
        prefabPic = Resources.Load<GameObject>("Prefabs/pic/pic");

    }

    public static void HideLayer(int n)
    {
        if (lstLayer.ContainsKey(n))
        {
            GameObject objLayer = lstLayer[n]; ;
            foreach (Transform child in objLayer.transform)
            {
                UIMgr.DestroyObj(child.gameObject);
            }

            objLayer.SetActive(false);
        }
    }
  public static void HideAll( )
  {
    instance.canvas.SetActive(false);
  }
    public static void HideItem(string itemName )
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            Object2D info = GetItemInfo(itemName);
            if(info !=null) 
            DestroyObj(info.gameObject);
        }
    }


    public static Object2D GetItemInfo(string itemName)
    {
        if (uiItemList.ContainsKey(itemName))
            return uiItemList[itemName] ;

     //   Debug.LogError("没有加载 " + itemName);
        return null;
    }

    public static GameObject CreateLayer(int n )
    {
        if (lstLayer.ContainsKey(n))
            return lstLayer[n];

        GameObject obj = Instantiate(instance.prefabLayer, instance.canvas.transform);

        obj.name = "_Layer" + n;
        lstLayer[n] = obj;


        foreach (var layer in lstLayer.Values)
        {
            layer.transform.SetAsLastSibling();
        }
        return lstLayer[n];
    }

    public static GameObject CreatePic(GameObject parent)
    {
       GameObject obj= Instantiate(instance.prefabPic, parent.transform);
        return obj;
    }

     public static GameObject CreateButton(GameObject parent)
     {
         GameObject obj = Instantiate(instance.prefabBtn, parent.transform);
         return obj;
     }
    //public static GameObject CreateVideoPanel(GameObject parent)
    //{
    //    GameObject obj = Instantiate(instance.prefabVideo, parent.transform);
    //    return obj;
    //}
    public static void DestroyObj(GameObject  obj)
    {
        Destroy(obj);
        uiItemList.Remove(obj.name);
        RemoveFromList(obj.name);
    }

    // Update is called once per frame
    void Update()
    {
    }


    void OnGUI()
    {
        //RectTransform rt=canvas.GetComponent<RectTransform>();
        //GUI.Label(new Rect(500, 50, 500, 50), rt.sizeDelta.ToString());

        //CanvasScaler sc = canvas.GetComponent<CanvasScaler>();
        //GUI.Label(new Rect(500, 100, 500, 50), sc.scaleFactor.ToString());

    }

     
}
