using System.Collections;
using UnityEngine;

namespace Audio
{
  public class Voice : MonoBehaviour
  {

    public AudioSource audioSource;

    bool bAudioPaused = false;

    // Use this for initialization
    void Start()
    {
      if (audioSource == null)
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {
      if (bAudioPaused == false)
      {
        bAudioPaused = true;

        audioSource.Pause();

      }
      else
      {
        bAudioPaused = false;

        audioSource.Play();
      }
    }


    Coroutine coroutine = null;

    
    public AudioSource play(AudioClip clip, Vector3 pos)
    {
      if (audioSource == null)
        return null;
      //if (audioSource != null)
      //{
      //  audioSource.clip = clip;
      //  audioSource.transform.position = pos;
      //  audioSource.Play();
      //}
      //return audioSource;
      if (coroutine != null)
        StopCoroutine(coroutine);
      coroutine = null;

      if (audioSource.isPlaying)
      {
        audioSource.Stop();
      }

      audioSource.clip = clip;
      audioSource.transform.position = pos;
 
      coroutine = StartCoroutine(playAudio(pos));

      return audioSource;
    }

    IEnumerator playAudio(Vector3 pos)
    {
      audioSource.Play();
     // audioSource.transform.position = pos;
      float fTime = 0;
      yield return fTime;// new WaitForSeconds(audioSource.clip.length);


      while (fTime < audioSource.clip.length || bAudioPaused)
      {
        if (!bAudioPaused)
          fTime += Time.deltaTime;
        yield return null;
      }

      coroutine = null;
      Destroy(this.gameObject);
    }

 
  }
}
