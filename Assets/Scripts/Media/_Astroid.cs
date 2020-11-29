using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 参数方程：x=a*(cost)3,y=a*(sint)3 （t为参数）
/// </summary>
public class _Astroid : MonoBehaviour
{
  float fTime = 0.0f;
  float fRadius = 10.0f;

  // Start is called before the first frame update
  void Start()
  {

  }

  public void Init_Astroid(Vector3 posCeneter, float radius)
  {

  }

  // Update is called once per frame
  void Update()
  {
    fTime += Time.deltaTime;

    //星形线
    float x = fRadius * Mathf.Pow(Mathf.Sin(fTime * 0.2f), 3.0f);
    float z = fRadius * Mathf.Pow(Mathf.Cos(fTime * 0.2f), 3.0f);

    Vector3 pos = Camera.main.transform.TransformPoint(new Vector3(x, 0, z));

    transform.position = pos;
  }
}
