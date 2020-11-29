using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Circle
/// </summary>
public class _Circle : MonoBehaviour
{
  float fTime = 0.0f;
  float fRadius = 10.0f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    fTime += Time.deltaTime;
    //园
    float x = fRadius * Mathf.Sin(fTime);
    float z = fRadius * Mathf.Cos(fTime);

    Vector3 pos = Camera.main.transform.TransformPoint(new Vector3(x, 0, z));

    //music.setPos(pos);
    transform.position = pos;
  }
}
