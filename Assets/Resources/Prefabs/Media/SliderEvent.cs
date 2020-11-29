using UnityEngine;
using UnityEngine.EventSystems;
/// 

/// 继承 拖拽接口
/// 
public class SliderEvent : MonoBehaviour, IDragHandler, IEndDragHandler
{
  [SerializeField]
  public VideoFile toPlayVideo;        // 视频播放的脚本

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  /// 

  /// 给 Slider 添加开始拖拽事件
  /// 
  /// 
  public void OnDrag(PointerEventData eventData)
  {
    toPlayVideo.Pause();

     toPlayVideo.Seek(toPlayVideo.Time + eventData.delta.x);
  }

 

  /// 

  /// 给 Slider 添加结束拖拽事件
  /// 
  /// 
  public void OnEndDrag(PointerEventData eventData)
  {
    toPlayVideo.Play();
  }
}
