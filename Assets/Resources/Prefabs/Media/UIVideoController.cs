using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

[Serializable]
public class FloatEvent : UnityEvent<float>
{

}

public class UIVideoController : MonoBehaviour
{

  private VideoFile theVideo;


  [SerializeField]
  private Button PlayPauseBtn;
  [SerializeField]
  private Text PlayBtnTxt;

  [SerializeField]
  private Slider PositionSlider;
  [SerializeField]
  private Slider PreviewSlider;

  [SerializeField]
  private FloatEvent onSeeked = new FloatEvent();

  // Start is called before the first frame update
  void Start()
  {
    PlayPauseBtn.onClick.AddListener(ToggleIsPlaying);
    PositionSlider.onValueChanged.AddListener(SliderValueChanged);
  }

  private void OnDestroy()
  {
    PlayPauseBtn.onClick.RemoveListener(ToggleIsPlaying);
    PositionSlider.onValueChanged.RemoveListener(SliderValueChanged);
  }

  public void InitPlayer(VideoFile video)
  {
    theVideo = video;
  }

  // Update is called once per frame
  void Update()
  {
    if (theVideo.IsPlaying)
    {
      PreviewSlider.value = theVideo.NormalizedTime;
    }
  }

  private void PlayVideo(VideoClip clip)
  {
    if (clip == null)
    {
      return;
    }

    theVideo.PrepareForClip(clip);
    PlayBtnTxt.text = "Pause";
  }

  private void ToggleIsPlaying()
  {
    if (theVideo.IsPlaying)
    {
      theVideo.Pause();
      PlayBtnTxt.text = "Play";
    }
    else
    {
      theVideo.Play();
      PlayBtnTxt.text = "Pause";
    }
  }

  private void SliderValueChanged(float value)
  {

    Debug.Log("Seek " + value);
    theVideo.Seek(value);
    //onSeeked.Invoke(value);
  }
}
