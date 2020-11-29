using System.Collections;
using UnityEngine;

namespace Audio
{
  public class Sound : MonoBehaviour
  {
    public AudioSource audioSource;
    static Sound instance;
    private Coroutine coroutine;

    // Use this for initialization
    void Start()
    {
      //  DontDestroyOnLoad(this);
      instance = this;
      if (audioSource == null)
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(AudioClip clip)
    {
      if (audioSource != null)
      {
        audioSource.clip = clip;
        audioSource.Play();
      }
    }

    public AudioSource Play(AudioClip clip, Vector3 pos)
    {
      if (audioSource == null)
        return null; if (coroutine != null)
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

      while (fTime < audioSource.clip.length  )
      {
         
          fTime += Time.deltaTime;
        yield return null;
      }

      coroutine = null;
      Destroy(this.gameObject);
    }
 
  }
}